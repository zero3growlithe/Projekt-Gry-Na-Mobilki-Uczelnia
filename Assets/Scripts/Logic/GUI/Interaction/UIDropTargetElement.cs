using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDropTargetElement : MonoBehaviour, IDropHandler
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	private UIBehaviour TargetUIBehaviour {get; set;}

	#endregion

	#region FUNCTIONS

	public void OnDrop (PointerEventData data)
	{
		HandleOnDropEvent(data.pointerDrag);
	}

	protected virtual void Awake ()
	{
		TargetUIBehaviour = gameObject.GetComponent<UIBehaviour>();
	}

	protected virtual void HandleOnDropEvent (GameObject target)
	{
		UIEventSystem.Instance.NotifyOnUIBehaviourDrop(TargetUIBehaviour);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
