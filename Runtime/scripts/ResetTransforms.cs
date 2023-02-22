using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransforms : MonoBehaviour
{
	#region Public
	//public members go here

	public delegate void OnMoveObj(Vector3 pos, GameObject obj);
	public static event OnMoveObj onMoveObj;

	#endregion

	#region Private
	//private members go here
	private Dictionary<string, TForm> _ogTrasforms = new Dictionary<string, TForm>();

	private class TForm {
		public Vector3 position;
		public Quaternion rotation;
		public Vector3 localPosition;
		public Quaternion localRotation;
		public Vector3 localScale;

		public TForm()
		{
			position = Vector3.zero;
			localPosition = position;
			localScale = Vector3.one;
			rotation = Quaternion.identity;
			localRotation = Quaternion.identity;
		}
	}
	#endregion
	#region DebugVariables
	// debug variables go here

	#endregion
	// Place all unity Message Methods here like OnCollision, Update, Start ect. 
	#region Unity Messages 
	// Start is called before the first frame update
	void Start()
    {
        foreach(var tf in GetComponentsInChildren<Transform>()) {
			TForm t = new TForm();
			t.position = tf.position;
			t.localPosition = tf.localPosition;
			t.localRotation = tf.localRotation;
			t.rotation = tf.rotation;
			t.localScale = tf.localScale;
			_ogTrasforms.Add(tf.GetInstanceID().ToString(), t);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	#endregion
	#region Public Methods
	//Place your public methods here
	public void ResetAllTransforms(float seconds = 0)
	{
		StartCoroutine(Reset(seconds));
	}
	#endregion
	#region Private Methods
	//Place your public methods here
	private IEnumerator Reset(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		var tfs = GetComponentsInChildren<Transform>();
		foreach (var tf in tfs) {
			if (_ogTrasforms.ContainsKey(tf.GetInstanceID().ToString())) {
				tf.transform.position = _ogTrasforms[tf.GetInstanceID().ToString()].position;
				tf.transform.localPosition = _ogTrasforms[tf.GetInstanceID().ToString()].localPosition;
				tf.transform.localRotation = _ogTrasforms[tf.GetInstanceID().ToString()].localRotation;
				tf.transform.rotation = _ogTrasforms[tf.GetInstanceID().ToString()].rotation;
				tf.transform.localScale = _ogTrasforms[tf.GetInstanceID().ToString()].localScale;
				onMoveObj?.Invoke(_ogTrasforms[tf.GetInstanceID().ToString()].localPosition, tf.gameObject);

			}
		}

	}

	#endregion
}
