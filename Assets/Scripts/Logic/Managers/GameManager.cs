using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
	#region MEMBERS

	[SerializeField]
	private GameState currentGameState;
	[SerializeField]
	private bool isTimeStopped;

	#endregion

	#region PROPERTIES

	public GameState CurrentGameState {
		get {return currentGameState;}
		private set {currentGameState = value;}
	}
	public bool IsTimeStopped {
		get {return isTimeStopped;}
		private set {isTimeStopped = value;}
	}

	#endregion

	#region FUNCTIONS

	public void SetCurrentGameState (GameState state)
	{
		CurrentGameState = state;
	}

	public void SetTimeStopState (bool isStopped)
	{
		IsTimeStopped = isStopped;
		Time.timeScale = isStopped ? 0 : 1;
	}

	#endregion

	#region CLASS_ENUMS

	public enum GameState
	{
		MAIN_MENU,
		PAUSE_MENU,
		GAME
	}

	#endregion
}
