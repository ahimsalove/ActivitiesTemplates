using UnityEngine;
using Virtence.VText;

[RequireComponent(typeof(VText))]
public class VtextUpdater : MonoBehaviour
{
	#region Public
	//public members go here

	#endregion

	#region Private
	//private members go here
	private VText _vText;
	#endregion
	#region DebugVariables
	// debug variables go here

	#endregion
	// Place all unity Message Methods here like OnCollision, Update, Start ect. 
	#region Unity Messages 
	#endregion
	#region Public Methods
	//Place your public methods here
	public VertextUpdater() { }
	public void SetReference(ref VText vText)
	{
		_vText = vText;
	}
	public void UpdateText(string text) => UpdateText(text);
	public void UpdateText(string text, params object[] parameters)
	{
		if (parameters != null)
		{
			text = string.Format(text, parameters);
		}
		text = text.Replace("\\n", "\n");
		text = text.Replace("/n", "\n");
		_vText.SetText(text);
		_vText.Rebuild();
		return;

	}
	#endregion
	#region Private Methods
	//Place your public methods here


	#endregion
}
