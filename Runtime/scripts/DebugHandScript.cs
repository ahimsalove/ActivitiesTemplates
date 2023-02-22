using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHandScript : MonoBehaviour
{
	#region Public
	//public members go here
	#endregion

	#region Private
	//private members go here
	[SerializeField, ReadOnly]
	Vector3 _mousePos;
	[SerializeField, ReadOnly]
	Vector3 _mouseRealPos;
	[SerializeField]
	bool _hideObjectWhenHeadSetOn = true;
	[SerializeField]
	Vector2 _minMaxZPosition = new Vector2(0.3f, 0.55f);
	[SerializeField]
	float _steps = 0.1f;
	// kEY OR MOUSE BUTTON TO MOVE THE OBJECT BACK AND FORTH
	[SerializeField]
	KeyCode _moveForwardKey = KeyCode.UpArrow;	
	[SerializeField]
	KeyCode _moveBackKey = KeyCode.DownArrow;
	[SerializeField]
	KeyCode _interactKey = KeyCode.Mouse0;
	[SerializeField]
	KeyCode _freezeKey = KeyCode.F;
	[SerializeField]
	TMPro.TextMeshPro _DebugText;



	float _initalZ;
	bool _freeze = false;
	private float _mouseButtonValue = 0;
	private GameObject _lastCollection;
	// varible to store the distance between the camera and the object
	private float _distance;
	bool _isHit = false;

	#endregion
	#region DebugVariables
	// debug variables go here

	#endregion
	// Place all unity Message Methods here like OnCollision, Update, Start ect. 
	#region Unity Messages 
	// Start is called before the first frame update
	void Start()
    {
		// get the initial z position of the object from the camera
		_distance = transform.position.z;
		if (_distance > _minMaxZPosition.y)
			_distance = _minMaxZPosition.y;
		else if (_distance < _minMaxZPosition.x)
			_distance = _minMaxZPosition.x;
    }
	void LateUpdate()
	{
		if (!_hideObjectWhenHeadSetOn) return;
		GetComponentInChildren<MeshRenderer>().enabled = GameObject.Find("BindPoses") != null ? false : true;
		if (_DebugText != null) _DebugText.gameObject.SetActive(GetComponentInChildren<MeshRenderer>().enabled);
	}

    // Update is called once per frame
    void Update()
    {
		if (!GetComponentInChildren<MeshRenderer>().enabled) return;

		if (Input.GetKeyDown(KeyCode.F)) {
			_freeze = !_freeze;
		}
		if (_freeze) return;
		// get the mouse position in world space and apply it to the transform
		_mouseRealPos = Input.mousePosition;
		_mouseRealPos.z = transform.position.z;
		try {
			_mousePos = Camera.main.ScreenToWorldPoint(_mouseRealPos);
		} catch (System.Exception e) {
			return;
		}
        _mousePos.z = transform.position.z;
		// if back key is pressed move the object back 0.1f or if the forward key is pressed move the object forward 0.1f
		if (Input.GetKeyDown(_moveBackKey)) {
			_distance = Mathf.Clamp(_distance - _steps, _minMaxZPosition.x, _minMaxZPosition.y);			
		}
		else if (Input.GetKeyDown(_moveForwardKey)) {
			_distance = Mathf.Clamp(_distance + _steps, _minMaxZPosition.x, _minMaxZPosition.y);
		}

		transform.position = _mousePos;

		// reposotion the object to the _distance from the camera forward vector only on the z axis
		if (Vector3.Distance(transform.position, Camera.main.transform.position) != _distance) {
			Vector3 direction = (transform.position - Camera.main.transform.position).normalized;
			transform.position = Camera.main.transform.position + (direction * _distance);
		}
		float dist = _distance * 100;
		if (_DebugText != null) _DebugText.text = string.Format("{0}", dist.ToString("00"));

	}
    #endregion
    #region Public Methods
    //Place your public methods here

    #endregion
    #region Private Methods
    //Place your public methods here
 
    
    #endregion
}
