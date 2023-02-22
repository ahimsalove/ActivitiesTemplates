using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainEyeHieght : MonoBehaviour
{
    #region Public
    //public members go here

    #endregion

    #region Private
    //private members go here
	[SerializeField]
	private string _anchorName = "CenterEyeAnchor";
	private float _differece;
	private Transform _anchorTransform;
  private float _anchorY;

	#endregion
	#region DebugVariables
	// debug variables go here

	#endregion
	// Place all unity Message Methods here like OnCollision, Update, Start ect. 
	#region Unity Messages 
	// Start is called before the first frame update
	void Start()
    {
		_anchorTransform = GameObject.Find(_anchorName) != null ? GameObject.Find(_anchorName).transform : Camera.main.transform;
		_anchorY = _anchorTransform.position.y;
		_differece = transform.position.y - _anchorTransform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      if (_anchorY == _anchorTransform.position.y) return;
      // move the game object up to match the anchor - the difference
       transform.position = new Vector3(transform.position.x, _anchorTransform.position.y + _differece, transform.position.z);
        _anchorY = _anchorTransform.position.y;

    }
    #endregion
    #region Public Methods
    //Place your public methods here

    #endregion
    #region Private Methods
    //Place your public methods here
 
    
    #endregion
}
