using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

[DisallowMultipleComponent]
public class UIBehaviourEvents : UIEvents
{
	#region MEMBERS

	[Header("[ Base references ]")]
	[SerializeField]
	private UIBehaviour targetUIBehaviour;

	[Header("[ Extended settings ]")]
	[SerializeField]
	private bool simulatePressWhenPointerEnter = false;

	#endregion

	#region PROPERTIES

	// REFERENCES
	protected UIBehaviour TargetUIBehaviour {
		get {return targetUIBehaviour;}
		private set {targetUIBehaviour = value;}
	}

	protected bool SimulatePressWhenPointerEnter {
		get { return simulatePressWhenPointerEnter; }
	}

	#endregion

	#region FUNCTIONS

	public override void OnPointerClick (PointerEventData data)
	{
		base.OnPointerClick(data);

		if (PointerUpHandlerType == EventHandler.UNITY_CLICK)
		{
			NotifyOnUIElementClick();
		}
	}

	public override void OnPointerDown (PointerEventData data)
	{
		base.OnPointerDown(data);

		NotifyOnUIElementDown();

		IsHeldDown = true;
	}
	
	// workaround for Unity bug (custom OnPointerUp from TouchEventSystem)
	public override void OnPointerUp (TouchEventSystem.TouchData pointerData)
	{
		base.OnPointerUp(pointerData);
		
		if (PointerUpHandlerType != EventHandler.CUSTOM_UP)
		{
			return;
		}
		
		bool isFullClick = (PointerUpInHoverArea == false && PointerUpIfClicked == true && IsHeldDown == true);
		bool isOverButton = (PointerUpIfClicked == false && PointerUpInHoverArea == true && IsMouseOver == true);

		if (isFullClick == true || isOverButton == true)
		{
			NotifyOnUIElementUp();
		}

		IsHeldDown = false;
	}

	public override void OnPointerUp (PointerEventData data)
	{
		base.OnPointerUp(data);

		if (PointerUpHandlerType == EventHandler.CUSTOM_UP)
		{
			return;
		}

		IsHeldDown = false;

		NotifyOnUIElementUp();
	}

	public override void OnPointerEnter (PointerEventData data)
	{
		base.OnPointerEnter(data);

		IsMouseOver = true;

		NotifyOnUIElementEnter();
	}
	
	public override void OnPointerExit (PointerEventData data)
	{
		base.OnPointerExit(data);

		StartCoroutine(ChangeMouseOver(false));

		NotifyOnUIElementExit();
	}

	public override void OnBeginDrag(PointerEventData data)
	{
		base.OnBeginDrag(data);
		
		NotifyOnUIDragBegin();
	}

	public override void OnEndDrag(PointerEventData data)
	{
		base.OnEndDrag(data);
		
		NotifyOnUIDragEnd();
	}

	protected override void Awake ()
	{
		base.Awake();
	}

	protected override void Reset ()
	{
		base.Reset();	

		if (TargetUIBehaviour == null)
		{
			TargetUIBehaviour = gameObject.GetComponent<UIBehaviour>();
		}
	}

	protected override void NotifyOnUIElementClick ()
	{
		OnElementClick();

		TargetEventSystem.NotifyOnUIBehaviourClick(TargetUIBehaviour);
	}
	  
	protected override void NotifyOnUIElementDown ()
	{
		OnElementDown();

		TargetEventSystem.NotifyOnUIBehaviourDown(TargetUIBehaviour);
	}

	protected override void NotifyOnUIElementUp ()
	{
		OnElementUp();

		TargetEventSystem.NotifyOnUIBehaviourUp(TargetUIBehaviour);
	}

	protected override void NotifyOnUIElementEnter ()
	{
		OnElementEnter();

		TargetEventSystem.NotifyOnUIBehaviourEnter(TargetUIBehaviour);
	}

	protected override void NotifyOnUIElementExit ()
	{
		OnElementExit();

		TargetEventSystem.NotifyOnUIBehaviourExit(TargetUIBehaviour);
	}

	protected override void NotifyOnUIDragBegin()
	{
		OnElementDragBegin();
		
		TargetEventSystem.NotifyOnUIBehaviourDragBegin(TargetUIBehaviour);
	}

	protected override void NotifyOnUIDragEnd()
	{
		OnElementDragEnd();
		
		TargetEventSystem.NotifyOnUIBehaviourDragEnd(TargetUIBehaviour);
	}

	private IEnumerator ChangeMouseOver(bool state)
	{
		yield return new WaitForEndOfFrame();

		IsMouseOver = state;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion


}
