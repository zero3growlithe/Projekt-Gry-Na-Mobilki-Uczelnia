using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    #region MEMBERS

    public System.Action<GameplayItem> OnItemReachedEnd = delegate{};

	[SerializeField]
	private MultiplierFloat moveSpeed;
	[SerializeField]
	private Transform container;
	[SerializeField]
	private Transform endPoint;

    #endregion

    #region PROPERTIES

	public MultiplierFloat MoveSpeed {
		get {return moveSpeed;}
	}
	public Transform Container {
		get {return container != null ? container : transform;}
	}
	public Transform EndPoint {
		get {return endPoint;}
	}

	public bool IsEnabled {get; set;}

    #endregion

    #region FUNCTIONS

	public void SetActive (bool state)
	{
		IsEnabled = state;
	}

	public void SetMoveSpeedMultiplier (float value)
	{
		MoveSpeed.Multiplier = value;
	}

	public void ClearSpawnedItems ()
	{
		for (int i = 0; i < Container.childCount; i++)
		{
			Destroy(Container.GetChild(i).gameObject);
		}
	}

	protected void Update ()
	{
		if (IsEnabled == false)
		{
			return;
		}

		UpdateItems();
	}

	private void UpdateItems ()
	{
		GameplayItem item;
		Transform itemTransform;

		for (int i = 0; i < Container.childCount; i++)
		{
			item = Container.GetChild(i).GetComponent<GameplayItem>();
			itemTransform = item.transform;

			if (item.IsBeingDragged == true)
			{
				continue;
			}

			itemTransform.position = Vector3.MoveTowards(itemTransform.position, EndPoint.position, MoveSpeed.Value * Time.deltaTime);

			if ((EndPoint.position - itemTransform.position).magnitude == 0)
			{
				OnItemReachedEnd(item);
				itemTransform.SetParent(EndPoint);
			}
		}
	}

    #endregion

    #region CLASS_ENUMS

    #endregion
}
