using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class UIEventSystem : MonoBehaviour
{
	#region MEMBERS

	// EVENTS
	public static Action<Button> OnUIButtonClick = delegate{};
	public static Action<Button> OnUIButtonDown = delegate{};
	public static Action<Button> OnUIButtonUp = delegate{};
	public static Action<Button> OnUIButtonDragBegin = delegate{};
	public static Action<Button> OnUIButtonDragEnd = delegate{};

	public static Action<Selectable> OnUISelectableClick = delegate{};
	public static Action<Selectable> OnUISelectableDown = delegate{};
	public static Action<Selectable> OnUISelectableUp = delegate{};
	public static Action<Selectable> OnUISelectableDragBegin = delegate{};
	public static Action<Selectable> OnUISelectableDragEnd = delegate{};

	public static Action<UIBehaviour> OnUIBehaviourClick = delegate{};
	public static Action<UIBehaviour> OnUIBehaviourDown = delegate{};
	public static Action<UIBehaviour> OnUIBehaviourUp = delegate{};
	public static Action<UIBehaviour> OnUIBehaviourDragBegin = delegate{};
	public static Action<UIBehaviour> OnUIBehaviourDragEnd = delegate{};
	public static Action<UIBehaviour> OnUIBehaviourDrop = delegate{};

	public static Action<Selectable> OnUISelectableEnter = delegate{};
	public static Action<Selectable> OnUISelectableExit = delegate{};

	public static Action<UIBehaviour> OnUIBehaviourEnter = delegate{};
	public static Action<UIBehaviour> OnUIBehaviourExit = delegate{};

	public static Action<InputField> OnUIInputFieldFocus = delegate{};
	public static Action<InputField> OnUIInputFieldDefocus = delegate{};
    
	// INTERNAL
	private List<UIInteractablePanel> activeInteractablePanels = new List<UIInteractablePanel>();

	private List<Button> currentHeldButtons = new List<Button>();

	private List<UIBehaviour> currentHeldUIBehaviours = new List<UIBehaviour>();
	private List<UIBehaviour> currentHoverUIBehaviours = new List<UIBehaviour>();
	private List<UIBehaviour> currentDragUIBehaviours = new List<UIBehaviour>();

	#endregion

	#region PROPERTIES

	public static UIEventSystem Instance {get; private set;}

	// SHORTCUTS
	public bool IsPointerOverUI {
		get {return EventSystem.current.IsPointerOverGameObject();}
	}

	// VARIABLES
	public List<UIInteractablePanel> ActiveInteractablePanels {
		get {return activeInteractablePanels;}
	}

	public List<Button> CurrentHeldButtons {
		get {return currentHeldButtons;}
	}

	public List<UIBehaviour> CurrentHeldUIBehaviours {
		get {return currentHeldUIBehaviours;}
	}
	public List<UIBehaviour> CurrentHoverUIBehaviours {
		get {return currentHoverUIBehaviours;}
	}
	public List<UIBehaviour> CurrentDragUIBehaviours {
		get {return currentDragUIBehaviours;}
	}

	#endregion

	#region FUNCTIONS

	// INTERACTABLE PANELS
	public bool IsPositionOverUIPanel (Vector2 position, Camera targetCamera = null)
	{
		for (int i = 0; i < ActiveInteractablePanels.Count; i++)
		{
			UIInteractablePanel panel = ActiveInteractablePanels[i];
			
			if (panel.isActiveAndEnabled == true && panel.IsPositionOverRect(position, targetCamera) == true)
			{
				return true;
			}
		}

		return false;
	}

	public UIInteractablePanel GetUIPanelByPosition(Vector2 position, Camera targetCamera = null)
	{
		for (int i = 0; i < ActiveInteractablePanels.Count; i++)
		{
			UIInteractablePanel panel = ActiveInteractablePanels[i];

			if (panel.isActiveAndEnabled == true && panel.IsPositionOverRect(position, targetCamera) == true)
			{
				return panel;
			}
		}

		return null;
	}

	public void AddInteractablePanel (UIInteractablePanel panel)
	{
		ActiveInteractablePanels.Add(panel);

		UIInteractablePanelDepthComparer comparer = new UIInteractablePanelDepthComparer();
		
		ActiveInteractablePanels.Sort(comparer);
	}

	public void RemoveInteractablePanel (UIInteractablePanel panel)
	{
		ActiveInteractablePanels.Remove(panel);
	}

	// BUTTON EVENTS
	public void NotifyOnUIButtonClick (Button target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUIButtonClick(target);
	}
	
	public void NotifyOnUIButtonDown (Button target)
	{
		if (target == null)
		{
			return;
		}
		
		AddButtonToHeld(target);
		OnUIButtonDown(target);
	}

	public void NotifyOnUIButtonUp (Button target)
	{
		if (target == null)
		{
			return;
		}
		
		RemoveButtonFromHeld(target);
		OnUIButtonUp(target);
	}

	public void NotifyOnUIButtonDragBegin (Button target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUIButtonDragBegin(target);
	}

	public void NotifyOnUIButtonDragEnd (Button target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUIButtonDragEnd(target);
	}

	// SELECTABLE EVENTS
	public void NotifyOnUISelectableClick (Selectable target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUISelectableClick(target);
	}

	public void NotifyOnUISelectableDown (Selectable target)
	{
		if (target == null)
		{
			return;
		}
		
		AddSelectableToHeld(target);
		OnUISelectableDown(target);
	}

	public void NotifyOnUISelectableUp (Selectable target)
	{
		if (target == null)
		{
			return;
		}
		
		RemoveSelectableFromHeld(target);
		OnUISelectableUp(target);
	}


	public void NotifyOnUISelectableEnter (Selectable target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUISelectableEnter(target);
	}

	public void NotifyOnUISelectableExit (Selectable target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUISelectableExit(target);
	}

	public void NotifyOnUISelectableDragBegin (Selectable target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUISelectableDragBegin(target);
	}

	public void NotifyOnUISelectableDragEnd (Selectable target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUISelectableDragEnd(target);
	}

	// UI ELEMENTS EVENTS
	public void NotifyOnUIBehaviourClick (UIBehaviour target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUIBehaviourClick(target);
	}

	public void NotifyOnUIBehaviourDown (UIBehaviour target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUIBehaviourDown(target);
	}

	public void NotifyOnUIBehaviourUp (UIBehaviour target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUIBehaviourUp(target);
	}

	
	public void NotifyOnUIBehaviourEnter (UIBehaviour target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUIBehaviourEnter(target);
	}

	public void NotifyOnUIBehaviourExit (UIBehaviour target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUIBehaviourExit(target);
	}

	public void NotifyOnUIBehaviourDragBegin (UIBehaviour target)
	{
		if (target == null)
		{
			return;
		}
		
		AddSelectableToDragged(target);
		OnUIBehaviourDragBegin(target);
	}

	public void NotifyOnUIBehaviourDragEnd (UIBehaviour target)
	{
		if (target == null)
		{
			return;
		}
		
		RemoveSelectableFromDragged(target);
		OnUIBehaviourDragEnd(target);
	}

	public void NotifyOnUIBehaviourDrop (UIBehaviour target)
	{
		if (target == null)
		{
			return;
		}
		
		OnUIBehaviourDrop(target);
	}

	// INPUT FIELD EVENTS
	public void NotifyOnUIInputFieldFocus (InputField field)
	{
		OnUIInputFieldFocus(field);
	}

	public void NotifyOnUIInputFieldDefocus (InputField field)
	{
		OnUIInputFieldDefocus(field);
	}

	protected virtual void Awake ()
	{
		Instance = this;
	}

	protected virtual void Start ()
	{

	}
	
	protected virtual void OnDestroy ()
	{
		
	}

	// HELD BUTTONS
	private void AddButtonToHeld (Button target)
	{
		if (CurrentHeldButtons.Contains(target) == false)
		{
			CurrentHeldButtons.Add(target);
		}
	}

	private void RemoveButtonFromHeld (Button target)
	{
		if (CurrentHeldButtons.Contains(target) == true)
		{
			CurrentHeldButtons.Remove(target);
		}
	}

	// HELD SELECTABLES
	private void AddSelectableToHeld (UIBehaviour target)
	{
		if (CurrentHeldUIBehaviours.Contains(target) == false)
		{
			CurrentHeldUIBehaviours.Add(target);
		}
	}

	private void RemoveSelectableFromHeld (UIBehaviour target)
	{
		if (CurrentHeldUIBehaviours.Contains(target) == true)
		{
			CurrentHeldUIBehaviours.Remove(target);
		}
	}
	
	// HOVER SELECTABLES
	private void AddSelectableToUsed (UIBehaviour target)
	{
		if (CurrentHoverUIBehaviours.Contains(target) == false)
		{
			CurrentHoverUIBehaviours.Add(target);
		}
	}

	private void RemoveSelectableFromUsed (UIBehaviour target)
	{
		if (CurrentHoverUIBehaviours.Contains(target) == true)
		{
			CurrentHoverUIBehaviours.Remove(target);
		}
	}
	
	// DRAG SELECTABLES
	private void AddSelectableToDragged (UIBehaviour target)
	{
		if (CurrentDragUIBehaviours.Contains(target) == false)
		{
			CurrentDragUIBehaviours.Add(target);
		}
	}

	private void RemoveSelectableFromDragged (UIBehaviour target)
	{
		if (CurrentDragUIBehaviours.Contains(target) == true)
		{
			CurrentDragUIBehaviours.Remove(target);
		}
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
