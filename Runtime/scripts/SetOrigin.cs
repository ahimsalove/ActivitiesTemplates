using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOrigin : MonoBehaviour {
	[Header("Origin")]
	public Transform target;
	public bool usePath = false;
	public string path;

	[Header("Options")]
	public bool position = true;
	public Vector3 positionOffset = Vector3.zero;
	public bool rotation = true;
	public Vector3 rotationOffset = Vector3.zero;
	public bool update = false;

	[Tooltip("Should the other object be moved here instead of this being moved to the other object?")]
	public bool grab = false;
	public void ResetPlayer(float seconds = 0)
	{
		StartCoroutine(Reset(seconds));
	}
	private IEnumerator Reset(float seconds) {
		yield return new WaitForSeconds(seconds);
		if (Ready()) {
			Set();
		}
	}
	private void Start()
	{
		if (PlayerPrefs.GetFloat("HeightOffset")!=0) {
			if (position) positionOffset.y += PlayerPrefs.GetFloat("HeightOffset");
			else transform.position = new Vector3(transform.position.x, transform.position.y + PlayerPrefs.GetFloat("HeightOffset"), transform.position.z);
		}
		if (usePath) {
			target = GameObject.Find(path) != null ? GameObject.Find(path).transform : Camera.main.transform.parent;
		}

		if (Ready()) {
			Set();
		}
	}

	private void Update()
	{
		if (update && Ready()) {
			Set();
		}
	}

	private void Set()
	{
		if (!grab) {
			if (position) {
				transform.position = target.position + positionOffset;
			}

			if (rotation) {
				transform.eulerAngles = target.eulerAngles + rotationOffset;
			}
		} else {
			if (position) {
				target.position = transform.position + positionOffset;
			}

			if (rotation) {
				target.eulerAngles = transform.eulerAngles + rotationOffset;
			}
		}
	}

	private bool Ready()
	{
		if (target != null) {
			return true;
		} else {
			return false;
		}
	}
}
