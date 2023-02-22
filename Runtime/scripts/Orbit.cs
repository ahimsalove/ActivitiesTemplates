using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace orbiter {
	public class Orbit : MonoBehaviour {
		#region Public
		//public members go here

		#endregion

		#region Private
		//private members go here
		[SerializeField]
		private string _anchorName;
		private GameObject _anchor;
		[SerializeField]
		private float _duration = 1f;
		[SerializeField]
		private int _framesPerSecond = 30;
		private int _framesMultipled = 30;
		private bool _canRotate = false;
		private bool _isMoving = false;
		private float _anchorYrotation;
		private float _anchorYPreRotation;

		private float _ogoffset;
		private float _distance;

		private Transform _anchorTransform;
		#endregion
		#region DebugVariables
		// debug variables go here

		#endregion
		// Place all unity Message Methods here like OnCollision, Update, Start ect. 
		#region Unity Messages 
		// Start is called before the first frame update
		void Start()
		{
			StartCoroutine(WaitForLoad());
			MoveObject.onMoveObj += delegate (Vector3 pos, GameObject obj) {
				
				StopAllCoroutines();
				StartCoroutine(MoveThisObject(pos));
			};
			ResetTransforms.onMoveObj += delegate (Vector3 pos,GameObject obj) {
				if (obj != gameObject) return;
				StopAllCoroutines();
				StartCoroutine(MoveThisObject(pos));
			};

			_distance = transform.localPosition.z;
		}

		// Update is called once per frame
		void Update()
		{
			if (_anchor == null) return;

			_canRotate = _anchorTransform.eulerAngles.y != _anchorYrotation && !_isMoving;
			if (!_canRotate) return;
			_anchorYPreRotation = _anchorTransform.eulerAngles.y;

			StartCoroutine(RotateTo());
			// reposotion the object to the _distance from the camera forward vector only on the z axis
			if (Vector3.Distance(transform.position, _anchorTransform.position) != _distance) {
				Vector3 direction = (transform.position - _anchorTransform.position).normalized;
				transform.position = _anchorTransform.position + (direction * _distance);
			}
		}

		#endregion
		#region Public Methods
		//Place your public methods here

		#endregion
		#region Private Methods
		//Place your public methods here
		private IEnumerator MoveThisObject(Vector3 pos)
		{

			var time = 0;
			var duration = _duration + Time.time;
			// divide the duration by 30 frames pre second to get the time between frames
			var span = _duration / _framesMultipled;
			// find the distance to rotate each frame
			var step = pos.z / _framesMultipled;
			while (Time.time < duration) {
				yield return new WaitForSeconds(span);
				_distance = pos.z < _distance ? _distance - step : _distance + step;
			}
			
			StartCoroutine(RotateTo());
		}
		private IEnumerator WaitForLoad()
		{
			yield return new WaitForSeconds(0.5f);
			_ogoffset = 0;
			_anchor = GameObject.Find(_anchorName) != null ? GameObject.Find(_anchorName) :  Camera.main.gameObject;
			if (_anchor == null) {
				Debug.LogException(new System.Exception("Anchor not found"));
				throw new System.Exception("Anchor not found");
			}
			_anchorTransform = _anchor.transform;
			_anchorYrotation = _anchorTransform.eulerAngles.y;
			_anchorYPreRotation = _anchorYrotation;
			_framesMultipled = (int)(_framesPerSecond * _duration);
		}
		private IEnumerator RotateTo()
		{
			// get the angle the the anchor rotated on the y axis 
			var yRot = _anchorTransform.eulerAngles.y - _anchorYrotation + _ogoffset;
			// set direction
			if (yRot > 180) yRot = yRot - 360;
			if (yRot < -180) yRot = yRot + 360;

			//Debug.Log("angle: " + _anchorTransform.eulerAngles.y + ", moveangle: " + yRot);
			// rotate to yRot in _duration
			var duration = _duration + Time.time;
			// divide the duration by 30 frames pre second to get the time between frames
			var span = _duration / _framesMultipled;
			// find the distance to rotate each frame
			var step = yRot / _framesMultipled;
			_isMoving = true;

			while (Time.time < duration) {
				// rotate the anchor around the y axis
				_ogoffset -= step;

				transform.RotateAround(_anchorTransform.position, Vector3.up, step);
				yield return new WaitForSeconds(span);
				if (_anchorYPreRotation != _anchorTransform.eulerAngles.y) goto endFunc;
				//test if the object has gone past the offset baase on the forward vector of the anchor
				var offset = transform.position - _anchor.transform.position;

			}
			_ogoffset = 0;

			_anchorYrotation = _anchorTransform.eulerAngles.y;
		endFunc:

			_canRotate = false;
			_isMoving = false;

			yield return null;
		}
		#endregion
	}
}