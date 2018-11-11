using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS

	public void StartGame ()
	{
		GameModeManager.Instance.SpawnDefaultGameMode();
		GUIManager.Instance.SetInGameHUDState(state: true, hideOther: true);
	}

	public void ShowLeaderboards ()
	{

	}

	public void ShowOptions ()
	{
		
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
