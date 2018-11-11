using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameLossCondition : GameplayCondition
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS

	public override bool GetConditionState ()
	{
		if (GameModeManager.Instance.CurrentGameMode == null)
		{
			return false;
		}

		return ((DefaultGameMode)GameModeManager.Instance.CurrentGameMode).CurrentLivesCount == 0;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
