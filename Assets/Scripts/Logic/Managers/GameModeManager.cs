using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviourSingleton<GameModeManager>
{
	#region MEMBERS

	[Header("[ References ]")]
	[SerializeField]
	private GameMode mainGameMode;

	// EVENTS
	public System.Action OnGameModeSuccess = delegate{};
	public System.Action OnGameModeFail = delegate{};

	#endregion

	#region PROPERTIES

	public GameMode MainGameMode {
		get {return mainGameMode;}
	}

	public GameMode CurrentGameMode {get; private set;}

	#endregion

	#region FUNCTIONS

	public void SpawnDefaultGameMode ()
	{
		SpawnGameMode(MainGameMode);
	}

	public void SpawnGameMode (GameMode mode)
	{
		ClearCurrentGameMode();

		CurrentGameMode = Instantiate(mode, transform);

		CurrentGameMode.ResetToDefault();

		CurrentGameMode.OnGameWin += NotifyOnGameModeSuccess;
		CurrentGameMode.OnGameLoss += NotifyOnGameModeFail;
	}

	public T GetCurrentGameMode<T>() where T : GameMode
	{
		return CurrentGameMode as T;
	}

	public void ClearCurrentGameMode ()
	{
		if (CurrentGameMode != null)
		{
			DestroyImmediate(CurrentGameMode.gameObject);
		}
	}

	public void RestartCurrentGameMode ()
	{
		if (CurrentGameMode != null)
		{
			CurrentGameMode.ResetToDefault();
		}
	}

	protected void Update ()
	{
		if (CurrentGameMode != null && CurrentGameMode.IsActive == true)
		{
			CurrentGameMode.GameModeTick();
		}
	}

	private void NotifyOnGameModeSuccess ()
	{
		OnGameModeSuccess();
	}

	private void NotifyOnGameModeFail ()
	{
		OnGameModeFail();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
