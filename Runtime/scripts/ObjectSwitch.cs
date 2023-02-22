using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitch : MonoBehaviour
{
	#region Public
	//public members go here
	#endregion

	#region Private
	//private members go here
	[SerializeField]
    KeyCode _switchKey = KeyCode.KeypadMultiply;
    [SerializeField]
    KeyCode _dimmerUpKey = KeyCode.KeypadPlus;
    [SerializeField]
    KeyCode _dimmerDownKey = KeyCode.KeypadMinus;
	[SerializeField, Range(0.01f, 1f), Tooltip("The number of steps the used to move the material alpha value by.")]
	float _dimmingStep = 0.1f;

    #endregion
    #region DebugVariables
    // debug variables go here

    #endregion
    // Place all unity Message Methods here like OnCollision, Update, Start ect. 
    #region Unity Messages 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_switchKey))
        {
			SwitchOnOff();
        }
        if (Input.GetKeyDown(_dimmerUpKey))
        {
			ChangeOpacity(GetComponent<MeshRenderer>().material.color.a + _dimmingStep);
        }
        if (Input.GetKeyDown(_dimmerDownKey))
        {
			ChangeOpacity(GetComponent<MeshRenderer>().material.color.a - _dimmingStep);

		}
	}
	#endregion
	#region Public Methods
	//Place your public methods here
	public void ChangeOpacity(int value)
	{
		// clamp the aplha value between 0 and 1
		var aplha = Mathf.Clamp(value*0.01f, 0, 1);
		GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, aplha);
	}
	public void ChangeOpacity(float value) => GetComponent<MeshRenderer>().material.color += new Color(0, 0, 0, value);

public void SwitchOnOff()
	{
		GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
	}
	#endregion
	#region Private Methods
	//Place your public methods here


	#endregion
}
