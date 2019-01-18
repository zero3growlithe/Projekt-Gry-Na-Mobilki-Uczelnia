using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHUDController : MonoBehaviourSingleton<GameHUDController>
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private UIImageCounter livesCounter;
	[SerializeField]
	private UIFormattedValue scoreCounter;

	#endregion

	#region PROPERTIES

	private UIImageCounter LivesCounter {
		get {return livesCounter;}
	}
	private UIFormattedValue ScoreCounter {
		get {return scoreCounter;}
	}

	#endregion

	#region FUNCTIONS

	public void HandlePauseButton ()
	{
		GUIManager.Instance.SetPauseMenuState(true);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
