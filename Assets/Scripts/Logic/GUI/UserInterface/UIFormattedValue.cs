using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFormattedValue : MonoBehaviour
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private Text targetText;
	[SerializeField]
	private string textFormat = "{0}:000000"; 

	#endregion

	#region PROPERTIES

	private Text TargetText {
		get {return targetText;}
	}
	private string TextFormat {
		get {return textFormat;}
	}

	#endregion

	#region FUNCTIONS

	public void SetValue (int value)
	{
		TargetText.text = string.Format(TextFormat, value);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
