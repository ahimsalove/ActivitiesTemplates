using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightProfile : MonoBehaviour
{
    #region Public
    //public members go here
    public enum HeightPresets{
        Average = 1250,
        Short = 1200,
        Tall = 1300,
        Custom
    }
    #endregion

    #region Private
    //private members go here
    [SerializeField]
	private string _anchorName = "OVRCameraRig";
    [SerializeField]
    private HeightPresets _preset = HeightPresets.Average;
    [SerializeField, Range(1.1f, 1.6f)]
    private float _customHeight = 1.415f;

    private float _heightInMm;
    private float _height;
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
        // convert the height form mm to meters
        AdjustHeight();
    }

    private void AdjustHeight()
    {
        _heightInMm = _preset == HeightPresets.Custom ? _customHeight * 1000 : (float)_preset;
        _height = _heightInMm / 1000;
        _anchorTransform = GameObject.Find(_anchorName).transform;
        _anchorTransform.localPosition = new Vector3(_anchorTransform.localPosition.x, _height, _anchorTransform.localPosition.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((_heightInMm / 1000) == _height || _height == _customHeight) return;
        AdjustHeight();        
    }
    #endregion
    #region Public Methods
    //Place your public methods here
    public void SetHeight(float height)
    {
        _preset = HeightPresets.Custom;
        _customHeight = height;
    }
    public void SetHeight(HeightPresets preset)
    {
        _preset = preset;
    }
    #endregion
    #region Private Methods
    //Place your public methods here
 
    
    #endregion
}
