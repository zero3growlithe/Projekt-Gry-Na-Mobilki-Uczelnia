using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltsManager : MonoBehaviourSingleton<ConveyorBeltsManager>
{
	#region MEMBERS

	public System.Action<GameplayItem> OnItemReachBeltEnd = delegate{};

	[SerializeField]
	private ConveyorBelt[] beltsCollection;
	[SerializeField]
	private ObjectSpawner[] spawnersCollection;

	#endregion

	#region PROPERTIES

	private ConveyorBelt[] BeltsCollection {
		get {return beltsCollection;}
	}
	private ObjectSpawner[] SpawnersCollection {
		get {return spawnersCollection;}
	}

	#endregion

	#region FUNCTIONS

	public void SetState (bool state)
	{
		// worst code ever... whatevs
		for (int i = 0; i < SpawnersCollection.Length; i++)
		{
			SpawnersCollection[i].SetActive(state);
		}

		for (int i = 0; i < BeltsCollection.Length; i++)
		{
			BeltsCollection[i].SetActive(state);
		}
	}

	public void ResetBelts ()
	{
		for (int i = 0; i < BeltsCollection.Length; i++)
		{
			BeltsCollection[i].ClearSpawnedItems();
		}
	}

	public void SetSpawnRateMultiplier (float value)
	{
		for (int i = 0; i < SpawnersCollection.Length; i++)
		{
			SpawnersCollection[i].SpawnFrequency.Multiplier = value;
		}
	}

	public void SetMoveMultiplier (float value)
	{
		for (int i = 0; i < BeltsCollection.Length; i++)
		{
			BeltsCollection[i].MoveSpeed.Multiplier = value;
		}
	}

	public void SetItemsCollection (GameplayItem[] collection)
	{
		for (int i = 0; i < SpawnersCollection.Length; i++)
		{
			SpawnersCollection[i].SetObjectsCollection(collection);
		}
	}

	protected override void Awake ()
	{
		base.Awake();
		
		AttachToEvents();
	}

	protected void OnDestroy ()
	{
		DetachFromEvents();
	}

	private void AttachToEvents ()
	{
		for (int i = 0; i < BeltsCollection.Length; i++)
		{
			BeltsCollection[i].OnItemReachedEnd += NotifyOnItemReachEnd;
		}
	}
	
	private void DetachFromEvents ()
	{
		for (int i = 0; i < BeltsCollection.Length; i++)
		{
			BeltsCollection[i].OnItemReachedEnd -= NotifyOnItemReachEnd;
		}
	}

	private void NotifyOnItemReachEnd (GameplayItem item)
	{
		OnItemReachBeltEnd(item);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
