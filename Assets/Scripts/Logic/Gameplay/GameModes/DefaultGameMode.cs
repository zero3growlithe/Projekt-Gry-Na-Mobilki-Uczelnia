using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultGameModeControllers;

public class DefaultGameMode : GameMode
{
	#region MEMBERS

	[Header("[ Values ]")]
	[SerializeField]
	private int livesCount = 3;
	[SerializeField]
	private int currentLivesCount = 0;
	[SerializeField]
	private int maxLivesCount = 5;

	[Header("[ Difficulty ]")]
	[SerializeField]
	private ItemsSpawnCollection[] itemsCollections;
	[SerializeField]
	private MultiplierCurve itemsSpeedToTime = new MultiplierCurve(0, 1, 1, 2);
	[SerializeField]
	private MultiplierCurve itemsSpawnRateToTime = new MultiplierCurve(0, 1, 1, 2);
	[SerializeField]
	private MultiplierCurve itemCollectionIncreaseToTime = new MultiplierCurve(0, 1, 1, 2);

	#endregion

	#region PROPERTIES

	// VALUES
	public int LivesCount {
		get {return livesCount;}
	}
	public int CurrentLivesCount {
		get {return currentLivesCount;}
		private set {currentLivesCount = value;}
	}
	public int MaxLivesCount {
		get {return maxLivesCount;}
	}

	// DIFFICULTY
	private MultiplierCurve ItemsSpeedToTime {
		get {return itemsSpeedToTime;}
	}
	private MultiplierCurve ItemsSpawnRateToTime {
		get {return itemsSpawnRateToTime;}
	}
	private MultiplierCurve ItemCollectionIncreaseToTime {
		get {return itemCollectionIncreaseToTime;}
	}

	#endregion

	#region FUNCTIONS

	public void AddLives (int count)
	{
		CurrentLivesCount = Mathf.Clamp(CurrentLivesCount + count, 0, MaxLivesCount);
	}

	public override void ResetToDefault()
	{
		base.ResetToDefault();

		CurrentLivesCount = LivesCount;
	}

	public override void GameModeTick ()
	{
		base.GameModeTick();

		UpdateHUD();
	}

	private void UpdateHUD()
	{
		if (GameHUDController.Instance == null)
		{
			return;
		}

		GameHUDController.Instance.UpdateLivesLabel(CurrentLivesCount);
		GameHUDController.Instance.UpdateScoreLabel(ScoreController.Instance.CurrentScore);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
