using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DragableUIElement : UIBehaviourEvents
{
	#region MEMBERS

	[SerializeField]
	private bool interactable = true;

	#endregion

	#region PROPERTIES

	public bool Interactable {
		get {return interactable;}
		set {interactable = value;}
	}

	public bool IsBeingDragged {get; protected set;}

	private RectTransform RootRectTransform {get; set;}
	private RectTransform ParentBeforeDrag {get; set;}
	private RectTransform CachedRectTransform {get; set;}

	private int DragBeginTouchID {get; set;}
	private Vector2 DragBeginTouchPosition {get; set;}
	private Vector2 DragBeginElementPosition {get; set;}
	private CanvasGroup CachedCanvasGroup {get; set;}

	#endregion

	#region FUNCTIONS

	protected override void Awake ()
	{
		base.Awake();

		CachedRectTransform = transform.GetComponent<RectTransform>();
		RootRectTransform = transform.root.GetComponent<RectTransform>();
		CachedCanvasGroup = transform.GetComponent<CanvasGroup>();
	}

	protected override void NotifyOnUIElementDown()
	{
		base.NotifyOnUIElementDown();

		ParentBeforeDrag = transform.parent.GetComponent<RectTransform>();
		DragBeginTouchPosition = LastEventPointerData.position;
		DragBeginTouchID = LastEventPointerData.pointerId;
		DragBeginElementPosition = CachedRectTransform.localPosition;
	}

	protected override void NotifyOnUIElementUp()
	{
		base.NotifyOnUIElementUp();
	}
	
	protected override void NotifyOnUIDragBegin()
	{
		base.NotifyOnUIDragBegin();

		if (Interactable == true)
		{
			HandleBeginDragEvent();
		}
	}

	protected override void NotifyOnUIDrag()
	{
		if (Interactable == false)
		{
			return;
		}

		TouchEventSystem.TouchData currentData = TouchEventSystem.Instance.GetTouchByFingerID(DragBeginTouchID);

		CachedRectTransform.position = currentData.Position;
	}
	
	protected override void NotifyOnUIDragEnd()
	{
		base.NotifyOnUIDragEnd();

		IsBeingDragged = false;
		CachedCanvasGroup.blocksRaycasts = true;
	}

	private void HandleBeginDragEvent()
	{
		IsBeingDragged = true;
		CachedCanvasGroup.blocksRaycasts = false;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
