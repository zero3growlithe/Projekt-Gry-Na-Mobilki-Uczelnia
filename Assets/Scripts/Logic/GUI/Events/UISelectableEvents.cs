using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

[DisallowMultipleComponent]
public class UISelectableEvents : UIBehaviourEvents
{
	#region MEMBERS

	[Header("[ Extended references ]")]
	[SerializeField]
	private Selectable targetUISelectable;

	#endregion

	#region PROPERTIES

	// REFERENCES
	protected Selectable TargetUISelectable {
		get {return targetUISelectable;}
		private set {targetUISelectable = value;}
	}

	#endregion

	#region FUNCTIONS

	protected override void Awake ()
	{
		base.Awake();
	}

	protected override void Reset ()
	{
		base.Reset();	

		if (TargetUISelectable == null)
		{
			TargetUISelectable = gameObject.GetComponent<Selectable>();
		}
	}

	protected override void NotifyOnUIElementClick ()
	{
		OnElementClick();
		
		TargetEventSystem.NotifyOnUISelectableClick(TargetUISelectable);
		TargetEventSystem.NotifyOnUIBehaviourClick(TargetUISelectable);
	}
	  
	protected override void NotifyOnUIElementDown ()
	{
		OnElementDown();
		
		TargetEventSystem.NotifyOnUISelectableDown(TargetUISelectable);
		TargetEventSystem.NotifyOnUIBehaviourDown(TargetUISelectable);

		TargetEventSystem.NotifyOnUIBehaviourDown(TargetUIBehaviour);
	}

	protected override void NotifyOnUIElementUp ()
	{
		OnElementUp();
		
		TargetEventSystem.NotifyOnUISelectableUp(TargetUISelectable);
		TargetEventSystem.NotifyOnUIBehaviourUp(TargetUISelectable);

		TargetEventSystem.NotifyOnUIBehaviourUp(TargetUIBehaviour);
	}

	protected override void NotifyOnUIElementEnter ()
	{
		OnElementEnter();
		
		TargetEventSystem.NotifyOnUISelectableEnter(TargetUISelectable);
		TargetEventSystem.NotifyOnUIBehaviourEnter(TargetUISelectable);

		TargetEventSystem.NotifyOnUIBehaviourEnter(TargetUIBehaviour);
	}

	protected override void NotifyOnUIElementExit ()
	{
		OnElementExit();
		
		TargetEventSystem.NotifyOnUISelectableExit(TargetUISelectable);
		TargetEventSystem.NotifyOnUIBehaviourExit(TargetUISelectable);

		TargetEventSystem.NotifyOnUIBehaviourExit(TargetUIBehaviour);
	}

	protected override void NotifyOnUIDragBegin()
	{
		OnElementDragBegin();

		TargetEventSystem.NotifyOnUIBehaviourDragBegin(TargetUIBehaviour);
		TargetEventSystem.NotifyOnUISelectableDragBegin(TargetUISelectable);
	}

	protected override void NotifyOnUIDragEnd()
	{
		OnElementDragEnd();

		TargetEventSystem.NotifyOnUIBehaviourDragEnd(TargetUIBehaviour);
		TargetEventSystem.NotifyOnUISelectableDragEnd(TargetUISelectable);
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
