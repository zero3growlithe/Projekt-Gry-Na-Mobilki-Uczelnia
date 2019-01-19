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
	private GameplayItem[] baseItemsCollection;
	[SerializeField]
	private GameplayItem[] additionalItemsCollection;

	[Space(5)]
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
	private GameplayItem[] BaseItemsCollection {
		get {return baseItemsCollection;}
	}
	private GameplayItem[] AdditionalItemsCollection {
		get {return additionalItemsCollection;}
	}

	private MultiplierCurve ItemsSpeedToTime {
		get {return itemsSpeedToTime;}
	}
	private MultiplierCurve ItemsSpawnRateToTime {
		get {return itemsSpawnRateToTime;}
	}
	private MultiplierCurve ItemCollectionIncreaseToTime {
		get {return itemCollectionIncreaseToTime;}
	}

	private int LastItemAddCount {get; set;}
	private float GameTime {get; set;}
	private List<GameplayItem> CurrentItemsCollection {get; set;}

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
		GameTime = 0;
		LastItemAddCount = 0;
		SetActiveState(true);

		CurrentItemsCollection = new List<GameplayItem>();
		CurrentItemsCollection.AddRange(BaseItemsCollection);
		ConveyorBeltsManager.Instance.SetItemsCollection(CurrentItemsCollection.ToArray());

		ConveyorBeltsManager.Instance.ResetBelts();
		ConveyorBeltsManager.Instance.SetState(true);
	}

	public override void GameModeTick ()
	{
		base.GameModeTick();

		// control multipliers
		ConveyorBeltsManager.Instance.SetSpawnRateMultiplier(1f + ItemsSpeedToTime.Evaluate(GameTime));
		ConveyorBeltsManager.Instance.SetMoveMultiplier(1f + ItemsSpawnRateToTime.Evaluate(GameTime));

		// add items
		int currentItemCount = (int)Mathf.Floor(ItemCollectionIncreaseToTime.Evaluate(GameTime));

		if (currentItemCount > LastItemAddCount)
		{
			CurrentItemsCollection.Add(AdditionalItemsCollection[UnityEngine.Random.Range(0, AdditionalItemsCollection.Length)]);
			ConveyorBeltsManager.Instance.SetItemsCollection(CurrentItemsCollection.ToArray());
			
			LastItemAddCount = (int)currentItemCount;
		}
	}

	protected virtual void Awake ()
	{
		ConveyorBeltsManager.Instance.OnItemReachBeltEnd += HandleOnItemReachBeltEndEvent;
	}

	protected virtual void OnDestroy ()
	{
		ConveyorBeltsManager.Instance.OnItemReachBeltEnd -= HandleOnItemReachBeltEndEvent;
	}

	protected override void HandleGameLossEvent()
	{
		ConveyorBeltsManager.Instance.SetState(false);
		GUIManager.Instance.SetInGameHUDState(false);
		GUIManager.Instance.SetGameOverScreenState(true);
	}

	private void HandleOnItemReachBeltEndEvent(GameplayItem item)
	{
		AddLives(-1);
		ScoreController.Instance.ReportScore(-100);
		Destroy(item);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
