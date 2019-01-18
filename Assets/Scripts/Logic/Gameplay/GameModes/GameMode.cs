using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
	#region MEMBERS

	[Header("[ Base settings ]")]
	[SerializeField]
	private bool isActive = false;

	[SerializeField]
	private List<GameplayCondition> gameWinConditions = new List<GameplayCondition>();
	[SerializeField]
	private List<GameplayCondition> gameLossConditions = new List<GameplayCondition>();

	// EVENTS
	public System.Action OnGameWin = delegate { };
	public System.Action OnGameLoss = delegate { };

	#endregion

	#region PROPERTIES

	public bool IsActive {
		get { return isActive; }
		private set { isActive = value; }
	}

	private List<GameplayCondition> GameWinConditions {
		get { return gameWinConditions; }
	}
	private List<GameplayCondition> GameLossConditions {
		get { return gameLossConditions; }
	}

	#endregion

	#region FUNCTIONS

	public void SetActiveState(bool state)
	{
		IsActive = state;
	}

	public virtual void GameModeTick()
	{
		CheckWinConditions();
		CheckLossConditions();
	}

	public virtual void ResetToDefault ()
	{

	}

	protected virtual void HandleGameWinEvent ()
	{

	}

	protected virtual void HandleGameLossEvent ()
	{

	}

	private void CheckWinConditions()
	{
		for (int i = 0; i < GameWinConditions.Count; i++)
		{
			if (GameWinConditions[i].GetConditionState() == true)
			{
				IsActive = false;

				HandleGameWinEvent();
				OnGameWin();
			}
		}
	}

	private void CheckLossConditions()
	{
		for (int i = 0; i < GameLossConditions.Count; i++)
		{
			if (GameLossConditions[i].GetConditionState() == true)
			{
				IsActive = false;

				HandleGameLossEvent();
				OnGameLoss();
			}
		}
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
