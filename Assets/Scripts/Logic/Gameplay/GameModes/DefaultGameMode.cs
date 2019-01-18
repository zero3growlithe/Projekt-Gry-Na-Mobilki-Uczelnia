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

	#endregion

	#region PROPERTIES

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

	public override void UpdateGameMode ()
	{
		base.UpdateGameMode();

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

	protected override void Awake()
	{
		base.Awake();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
