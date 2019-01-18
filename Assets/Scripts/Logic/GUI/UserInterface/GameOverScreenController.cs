using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenController : MonoBehaviour
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS

	public void RetryGame ()
	{
		GameModeManager.Instance.SpawnDefaultGameMode();
		GameManager.Instance.SetTimeStopState(false);
		GUIManager.Instance.SetPauseMenuState(false);
	}

	public void BackToMenu ()
	{
		GameModeManager.Instance.ClearCurrentGameMode();
		GameManager.Instance.SetTimeStopState(false);
		GUIManager.Instance.SetMainMenuState(state: true, hideOther: true);
	}

	protected void OnEnable ()
	{
		GameManager.Instance.SetTimeStopState(true);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
