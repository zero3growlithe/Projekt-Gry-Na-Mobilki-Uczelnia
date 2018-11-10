using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

[DisallowMultipleComponent]
public class UIButtonEvents : UISelectableEvents
{
	#region MEMBERS

	[SerializeField]
	private Button targetButton;

	#endregion

	#region PROPERTIES

	// VARIABLES
	private Button TargetButton {
		get {return targetButton;}
		set {targetButton = value;}
	}
    
	#endregion

	#region FUNCTIONS
	
	protected override void Awake ()
	{
		base.Awake();

		if (TargetButton == null)
		{
			TargetButton = gameObject.GetComponent<Button>();
		}
	}

	protected override void Reset ()
	{
		base.Reset();	

		if (TargetButton == null)
		{
			TargetButton = gameObject.GetComponent<Button>();
		}
	}
   
	protected override void NotifyOnUIElementClick ()
	{
		OnElementClick();
		
		TargetEventSystem.NotifyOnUIButtonClick(TargetButton);
		TargetEventSystem.NotifyOnUISelectableClick(TargetButton);
		TargetEventSystem.NotifyOnUIBehaviourClick(TargetButton);

		TargetEventSystem.NotifyOnUISelectableClick(TargetUISelectable);
		TargetEventSystem.NotifyOnUIBehaviourClick(TargetUISelectable);

		TargetEventSystem.NotifyOnUIBehaviourClick(TargetUIBehaviour);
	}
   
	protected override void NotifyOnUIElementDown ()
	{
		OnElementDown();
		
		TargetEventSystem.NotifyOnUIButtonDown(TargetButton);
		TargetEventSystem.NotifyOnUISelectableDown(TargetButton);
		TargetEventSystem.NotifyOnUIBehaviourDown(TargetButton);

		TargetEventSystem.NotifyOnUISelectableDown(TargetUISelectable);
		TargetEventSystem.NotifyOnUIBehaviourDown(TargetUISelectable);

		TargetEventSystem.NotifyOnUIBehaviourDown(TargetUIBehaviour);
	}

	protected override void NotifyOnUIElementUp ()
	{
		OnElementUp();
		
		TargetEventSystem.NotifyOnUIButtonUp(TargetButton);
		TargetEventSystem.NotifyOnUISelectableUp(TargetButton);
		TargetEventSystem.NotifyOnUIBehaviourUp(TargetButton);

		TargetEventSystem.NotifyOnUISelectableUp(TargetUISelectable);
		TargetEventSystem.NotifyOnUIBehaviourUp(TargetUISelectable);

		TargetEventSystem.NotifyOnUIBehaviourUp(TargetUIBehaviour);
	}

	protected override void NotifyOnUIElementEnter ()
	{
		OnElementEnter();
		
		TargetEventSystem.NotifyOnUISelectableEnter(TargetButton);
		TargetEventSystem.NotifyOnUIBehaviourEnter(TargetButton);

		TargetEventSystem.NotifyOnUISelectableEnter(TargetUISelectable);
		TargetEventSystem.NotifyOnUIBehaviourEnter(TargetUISelectable);

		TargetEventSystem.NotifyOnUIBehaviourEnter(TargetUIBehaviour);
	}

	protected override void NotifyOnUIElementExit ()
	{
		OnElementExit();
		
		TargetEventSystem.NotifyOnUISelectableExit(TargetButton);
		TargetEventSystem.NotifyOnUIBehaviourExit(TargetButton);

		TargetEventSystem.NotifyOnUISelectableExit(TargetUISelectable);
		TargetEventSystem.NotifyOnUIBehaviourExit(TargetUISelectable);

		TargetEventSystem.NotifyOnUIBehaviourExit(TargetUIBehaviour);
	}

	protected override void NotifyOnUIDragBegin()
	{
		OnElementDragBegin();

		TargetEventSystem.NotifyOnUIBehaviourDragBegin(TargetUIBehaviour);
		TargetEventSystem.NotifyOnUISelectableDragBegin(TargetUISelectable);
		TargetEventSystem.NotifyOnUIButtonDragBegin(TargetButton);
	}

	protected override void NotifyOnUIDragEnd()
	{
		OnElementDragEnd();

		TargetEventSystem.NotifyOnUIBehaviourDragEnd(TargetUIBehaviour);
		TargetEventSystem.NotifyOnUISelectableDragEnd(TargetUISelectable);
		TargetEventSystem.NotifyOnUIButtonDragEnd(TargetButton);
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
