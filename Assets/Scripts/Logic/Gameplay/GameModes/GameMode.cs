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

	public void SetState(bool state)
	{
		IsActive = state;
	}

	public virtual void UpdateGameMode()
	{
		if (IsActive == false)
		{
			return;
		}

		CheckWinConditions();
		CheckLossConditions();
	}

	public virtual void ResetToDefault ()
	{

	}

	protected virtual void Awake ()
	{

	}

	protected virtual void Start ()
	{

	}

	private void CheckWinConditions()
	{
		for (int i = 0; i < GameWinConditions.Count; i++)
		{
			if (GameWinConditions[i].GetConditionState() == true)
			{
				IsActive = false;

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

				OnGameLoss();
			}
		}
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
