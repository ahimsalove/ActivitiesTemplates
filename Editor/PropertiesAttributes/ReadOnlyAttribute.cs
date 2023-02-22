using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows a property to be made read only in the Unity Inspector. Does not interfere with standard protection level.
/// </summary>
public class ReadOnlyAttribute : PropertyAttribute
{

}

//Source: https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html