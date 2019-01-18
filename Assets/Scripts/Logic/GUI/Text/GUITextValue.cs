using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUITextValue : ClassValue
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private Text targetText;

	[Header("[ Settings ]")]
	[SerializeField]
	private bool useLocalizedKey = true;
	[SerializeField]
	private bool formatAsNumber = false;
	[SerializeField]
	private string textFormatKey = "{0}";

	#endregion

	#region PROPERTIES

	// REFERENCES
	private Text TargetText {
		get {return targetText;}
		set {targetText = value;}
	}

	// SETTINGS
	private bool UseLocalizedKey {
		get {return useLocalizedKey;}
	}
	private bool FormatAsNumber {
		get {return formatAsNumber;}
	}
	private string TextFormatKey {
		get {return textFormatKey;}
	}

	#endregion

	#region FUNCTIONS

	protected virtual void Update ()
	{
		UpdateText();
	}

	protected virtual void Reset ()
	{
		if (TargetText == null)
		{
			TargetText = GetComponent<Text>();
		}
	}

	protected void UpdateText ()
	{
		if (TargetText == null)
		{
			return;
		}

		string localizedText = string.Empty;

		// get formatted text
		if (FormatAsNumber == false)
		{
			localizedText = string.Format(TextFormatKey, StringValue);
		}
		else
		{
			localizedText = string.Format(TextFormatKey, DoubleValue);
		}

		// update displayed text
		if (TargetText.text != localizedText)
		{
			TargetText.text = localizedText;
		}
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
