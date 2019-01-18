using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    #region MEMBERS

    public System.Action<DragableUIElement> OnItemReachedEnd = delegate{};

	[SerializeField]
	private MultiplierFloat moveSpeed;
	[SerializeField]
	private Transform endPoint;

    #endregion

    #region PROPERTIES

	public MultiplierFloat MoveSpeed {
		get {return moveSpeed;}
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
		DragableUIElement item;
		Transform itemTransform;

		// sigh....
		for (int i = 0; i < transform.childCount; i++)
		{
			item = transform.GetChild(i).GetComponent<DragableUIElement>();
			itemTransform = item.transform;

			if (item.IsBeingDragged == true)
			{
				continue;
			}

			itemTransform.position = Vector3.MoveTowards(itemTransform.position, EndPoint.position, MoveSpeed.Value * Time.deltaTime);

			if ((EndPoint.position - itemTransform.position).magnitude == 0)
			{
				OnItemReachedEnd(item);
			}
		}
	}

    #endregion

    #region CLASS_ENUMS

    #endregion
}
