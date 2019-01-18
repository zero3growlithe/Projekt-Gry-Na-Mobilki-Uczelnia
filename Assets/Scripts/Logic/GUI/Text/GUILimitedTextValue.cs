using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUILimitedTextValue : ClassLimitValue
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private Text targetCurrentValueText;
	[SerializeField]
	private Text targetLimitText;

	[Header("[ Settings ]")]
	[SerializeField]
	private bool useLocalizedKey = true;
	[SerializeField]
	private bool formatAsNumber = false;
	[SerializeField]
	private string unifiedTextFormatKey = "{0}";

	#endregion

	#region PROPERTIES

	// REFERENCES
	public Text TargetCurrentValueText {
		get {return targetCurrentValueText;}
		private set {targetCurrentValueText = value;}
	}
	public Text TargetLimitText {
		get {return targetLimitText;}
	}

	// SETTINGS
	protected bool UseLocalizedKey {
		get {return useLocalizedKey;}
	}
	protected bool FormatAsNumber {
		get {return formatAsNumber;}
	}
	protected string UnifiedTextFormatKey {
		get {return unifiedTextFormatKey;}
	}

	#endregion

	#region FUNCTIONS

	protected virtual void Update ()
	{
		UpdateText();
	}

	protected virtual void Reset ()
	{
		if (TargetCurrentValueText == null)
		{
			TargetCurrentValueText = GetComponent<Text>();
		}
	}

	protected void UpdateText ()
	{
		if (TargetCurrentValueText == null)
		{
			return;
		}

		bool splitValues = (TargetLimitText != null);

		// get formatted text
		if (splitValues == false)
		{
			UpdateText(TargetCurrentValueText, GetFormattedText());
		}
		else if (splitValues == true)
		{
			UpdateText(TargetCurrentValueText, CurrentStringValue);
			UpdateText(TargetLimitText, LimitStringValue);
		}
	}

	private void UpdateText (Text targetText, string value)
	{
		if (targetText.text != value)
		{
			targetText.text = value;
		}
	}

	private string GetFormattedText ()
	{
		if (FormatAsNumber == true)
		{
			return string.Format(UnifiedTextFormatKey, CurrentDoubleValue, LimitDoubleValue);
		}

		return string.Format(UnifiedTextFormatKey, CurrentStringValue, LimitStringValue);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
