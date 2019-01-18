using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultGameModeControllers;

public class CurrentScoreValue : GUITextValue
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	public override double DoubleValue {
		get { return ScoreController.Instance != null ? ScoreController.Instance.CurrentScore : 0; }
	}
	public override string StringValue {
		get { return DoubleValue.ToString();; }
	}

	#endregion

	#region FUNCTIONS

	#endregion

	#region CLASS_ENUMS

	#endregion
}
