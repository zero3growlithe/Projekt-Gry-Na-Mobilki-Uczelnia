using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultGameModeControllers;

public class CurrentLivesValue : ClassValue
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	public override double DoubleValue {
		get { return GameModeManager.Instance.GetCurrentGameMode<DefaultGameMode>().CurrentLivesCount; }
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
