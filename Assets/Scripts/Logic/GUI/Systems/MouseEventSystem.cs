using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class MouseEventSystem : MonoBehaviour
{
	#region MEMBERS

	public static Action<PointerData> OnMouseDrag = delegate{};
	public static Action<PointerData> OnMouseScroll = delegate{};

	public static Action<PointerData> OnMouseDown = delegate{};
	public static Action<PointerData> OnMouseUp = delegate{};
	public static Action<PointerData> OnMouse = delegate{};

	#endregion

	#region PROPERTIES

	public static MouseEventSystem Instance {get; private set;}

	// SHORTCUTS
	private Camera CurrentCamera {
		get {return Camera.current;}
	}

	// VARIABLES
	public PointerData CurrentPointerData {get; private set;}
	public State CurrentState {get; private set;}

	public bool IsDragging {get; private set;}

	#endregion

	#region FUNCTIONS

	protected virtual void Awake ()
	{
		Instance = this;
		CurrentPointerData = new PointerData();
	}

	protected virtual void Start ()
	{

	}
	
	protected virtual void Update ()
	{
		CheckInput();
	}
	
	protected virtual void OnDestroy ()
	{
		
	}

	private void CheckInput ()
	{
		// get mouse scroll wheel
		if (Input.mouseScrollDelta != Vector2.zero)
		{
			HandleMouseScroll();
		}

		// get mouse button hold
		if (Input.GetMouseButtonDown(0) == true)
		{
			CurrentState = State.DOWN;

			HandleMouseDown();
		}

		// get mouse button down
		if (Input.GetMouseButton(0) == true)
		{
			HandleMouseHold();
		}
		else
		{
			CurrentState = State.NONE;
			CurrentPointerData.Phase = State.NONE;
		}

		// get mouse button up
		if (Input.GetMouseButtonUp(0) == true)
		{
			CurrentState = State.UP;

			HandleMouseUp();
		}
	}

	private void HandleMouseDown ()
	{
		RaycastHit hit = GetRaycastData();
		GameObject clickedObject = (hit.collider != null) ? hit.collider.gameObject : null;

		CurrentPointerData.Hit = hit;
		CurrentPointerData.CurrentObject = clickedObject;
		CurrentPointerData.DragObject = clickedObject;

		CurrentPointerData.StartTime = Time.unscaledTime;
		CurrentPointerData.Position = Input.mousePosition;
		CurrentPointerData.LastPosition = CurrentPointerData.Position;
		CurrentPointerData.ClickPosition = Input.mousePosition;

		CurrentPointerData.Phase = State.DOWN;
		CurrentPointerData.Delta = Vector2.zero;

		IsDragging = false;

		OnMouseDown(CurrentPointerData);
	}

	private void HandleMouseUp ()
	{
		CurrentPointerData.LastPosition = CurrentPointerData.Position;
		CurrentPointerData.Position = Input.mousePosition;
		CurrentPointerData.Delta = Vector2.zero;

		CurrentPointerData.Phase = State.UP;

		IsDragging = false;

		OnMouseUp(CurrentPointerData);
	}

	private void HandleMouseHold ()
	{
		RaycastHit hit = GetRaycastData();

		CurrentPointerData.Hit = hit;
		CurrentPointerData.CurrentObject = (hit.collider != null) ? hit.collider.gameObject : null;
		CurrentPointerData.Position = Input.mousePosition;
		CurrentPointerData.Delta = CurrentPointerData.Position - CurrentPointerData.LastPosition;

		CurrentPointerData.LastPosition = CurrentPointerData.Position;
		CurrentPointerData.Phase = State.HOLD;

		// handle mouse drag
		IsDragging |=
			(CurrentPointerData.Position - CurrentPointerData.ClickPosition).magnitude >
			((EventSystem.current != null) ? EventSystem.current.pixelDragThreshold : 0f);

		if (IsDragging == true)
		{
			CurrentPointerData.Phase = State.DRAG;

			OnMouseDrag(CurrentPointerData);
		}

		// handle mouse hold
		OnMouse(CurrentPointerData);
	}

	private void HandleMouseScroll ()
	{
		PointerData scrollData = new PointerData();
		RaycastHit hit = GetRaycastData();
		GameObject clickedObject = (hit.collider != null) ? hit.collider.gameObject : null;

		scrollData.Hit = hit;
		scrollData.CurrentObject = clickedObject;
		scrollData.DragObject = clickedObject;

		scrollData.StartTime = Time.unscaledTime;
		scrollData.Position = Input.mousePosition;
		scrollData.LastPosition = scrollData.Position;
		scrollData.ClickPosition = Input.mousePosition;

		scrollData.Phase = State.NONE;
		scrollData.ScrollDelta = Input.mouseScrollDelta;

		OnMouseScroll(scrollData);
	}

	private RaycastHit GetRaycastData ()
	{
		if (CurrentCamera == null)
		{
			return new RaycastHit();
		}

		Ray cameraRay = CurrentCamera.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;

		Physics.Raycast(cameraRay, out hit, Mathf.Infinity);

		return hit;
	}

	#endregion

	#region CLASS_ENUMS

	public enum State {
		NONE,
		DOWN,
		HOLD,
		DRAG,
		UP
	}

	public class PointerData {
		public RaycastHit Hit {get; set;}
		public GameObject CurrentObject {get; set;}
		public GameObject DragObject {get; set;}

		public float StartTime {get; set;}
        public int FingerID { get; set; }

		public Vector3 Position {get; set;}
		public Vector3 LastPosition {get; set;}
		public Vector3 ClickPosition {get; set;}
		public Vector2 Delta {get; set;}
        
		public State Phase {get; set;}

		public Vector2 ScrollDelta {get; set;}
	}

	#endregion
}
