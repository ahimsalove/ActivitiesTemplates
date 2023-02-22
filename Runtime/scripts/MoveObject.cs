using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
	#region Public
	//public members go here
	public delegate void OnMoveObj(Vector3 pos, GameObject obj);
	public static event OnMoveObj onMoveObj;
	#endregion

	#region Private
	//private members go here
	Vector3 _originalPosition;
    #endregion
    #region DebugVariables
    // debug variables go here

    #endregion
    // Place all unity Message Methods here like OnCollision, Update, Start ect. 
    #region Unity Messages 
    // Start is called before the first frame update
    void Start()
    {
		_originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #region Public Methods
    //Place your public methods here
	public void MoveObjectZinCM(int distance)
	{
		float d = distance * 0.01f;
		Vector3 dist = new Vector3(transform.localPosition.x, transform.localPosition.y, d) ;
		transform.localPosition = dist;
		onMoveObj?.Invoke(dist, gameObject);
	}
    #endregion
    #region Private Methods
    //Place your public methods here
 
    
    #endregion
}
