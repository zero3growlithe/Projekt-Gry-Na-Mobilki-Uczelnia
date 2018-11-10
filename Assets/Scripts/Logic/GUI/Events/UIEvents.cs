using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class UIEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	#region MEMBERS

	[Header("[ Base settings ]")]
	[SerializeField]
	private EventHandler pointerUpHandlerType = EventHandler.UNITY_CLICK;
	[SerializeField]
	[Tooltip("Detect pointer up only if last button down was on the button itself")]
	private bool pointerUpIfClicked = true;
	[SerializeField]
	[Tooltip("Detect pointer up only if the mouse is over the button")]
	private bool pointerUpInHoverArea = true;

	// EVENTS
	public System.Action OnElementClick = delegate{};
	public System.Action OnElementDown = delegate{};
	public System.Action OnElementUp = delegate{};
	public System.Action OnElementEnter = delegate{};
	public System.Action OnElementExit = delegate{};
	public System.Action OnElementDragBegin = delegate{};
	public System.Action OnElementDrag = delegate{};
	public System.Action OnElementDragEnd = delegate{};

	#endregion

	#region PROPERTIES

	// SETTINGS
	protected EventHandler PointerUpHandlerType {
		get {return pointerUpHandlerType;}
	}
	protected bool PointerUpIfClicked {
		get {return pointerUpIfClicked;}
	}
	protected bool PointerUpInHoverArea {
		get {return pointerUpInHoverArea;}
	}

	// VARIABLES
	protected bool IsHeldDown { get; set; }

	protected bool IsMouseOver { get; set; }

	// SHORTCUTS
	protected UIEventSystem TargetEventSystem {
		get { return UIEventSystem.Instance; }
	}

	protected PointerEventData LastEventPointerData {get; set;}

	#endregion

	#region FUNCTIONS
	
	public virtual void OnPointerClick (PointerEventData data)
	{
		LastEventPointerData = data;
	}
	
	public virtual void OnPointerDown (PointerEventData data)
	{
		LastEventPointerData = data;
	}
	
	// workaround for Unity bug
	public virtual void OnPointerUp (TouchEventSystem.TouchData pointerData)
	{
		
	}

	public virtual void OnPointerUp (PointerEventData data)
	{
		LastEventPointerData = data;
	}

	public virtual void OnPointerEnter (PointerEventData data)
	{
		LastEventPointerData = data;
	}
	
	public virtual void OnPointerExit (PointerEventData data)
	{
		LastEventPointerData = data;
	}

	public virtual void OnBeginDrag(PointerEventData data)
	{
		LastEventPointerData = data;
		
		NotifyOnUIDragBegin();
	}

	public virtual void OnDrag(PointerEventData data)
	{
		NotifyOnUIDrag();
	}

	public virtual void OnEndDrag(PointerEventData data)
	{
		LastEventPointerData = data;
		
		NotifyOnUIDragEnd();
	}

	protected virtual void Awake ()
	{

	}

	protected virtual void Reset ()
	{
		
	}
	
	protected virtual void OnEnable ()
	{
		if (PointerUpHandlerType == EventHandler.CUSTOM_UP)
		{
			TouchEventSystem.OnTouchUp += OnPointerUp;
		}
	}

	protected virtual void OnDisable ()
	{
		if (PointerUpHandlerType == EventHandler.CUSTOM_UP)
		{
			TouchEventSystem.OnTouchUp -= OnPointerUp;
		}

		IsHeldDown = false;
		IsMouseOver = false;
	}

	protected virtual void NotifyOnUIElementClick ()
	{

	}
	  
	protected virtual void NotifyOnUIElementDown ()
	{

	}

	protected virtual void NotifyOnUIElementUp ()
	{

	}

	protected virtual void NotifyOnUIElementEnter ()
	{

	}

	protected virtual void NotifyOnUIElementExit ()
	{

	}

	protected virtual void NotifyOnUIDragBegin ()
	{

	}

	protected virtual void NotifyOnUIDrag ()
	{
		OnElementDrag();
	}

	protected virtual void NotifyOnUIDragEnd ()
	{

	}

	#endregion

	#region CLASS_ENUMS

	public enum EventHandler
	{
		UNITY_CLICK,
		UNITY_UP,
		CUSTOM_UP
	}

	#endregion


}
