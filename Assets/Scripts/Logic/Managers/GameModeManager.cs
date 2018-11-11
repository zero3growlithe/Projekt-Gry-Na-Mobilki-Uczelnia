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

		CurrentGameMode = Instantiate(MainGameMode, transform);

		CurrentGameMode.ResetToDefault();

		CurrentGameMode.OnGameWin += NotifyOnGameModeSuccess;
		CurrentGameMode.OnGameLoss += NotifyOnGameModeFail;
	}

	public void ClearCurrentGameMode ()
	{
		if (CurrentGameMode != null)
		{
			Destroy(CurrentGameMode.gameObject);
		}
	}

	protected void Update ()
	{
		if (CurrentGameMode != null)
		{
			CurrentGameMode.UpdateGameMode();
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
