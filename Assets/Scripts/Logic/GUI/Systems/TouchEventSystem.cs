using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchEventSystem : MonoBehaviour
{
	#region MEMBERS

	// ACTIONS
	public static System.Action<TouchData> OnTouchDown = delegate { };
	public static System.Action<TouchData> OnTouchHold = delegate { };
	public static System.Action<TouchData> OnTouchMove = delegate { };
	public static System.Action<TouchData> OnTouchUp = delegate { };

	public static System.Action OnBeginUpdateTouch = delegate { };
	public static System.Action OnEndUpdateTouch = delegate { };

	#endregion

	#region PROPERTIES

	// STATIC VARIABLES
	public static TouchEventSystem Instance { get; private set; }

	// PUBLIC VARIABLES
	public int LastFingerID { get; private set; }

	#endregion

	#region FUNCTIONS

	public bool IsTouchWithIDActive(int id)
	{
		for (int i = 0; i < Input.touches.Length; i++)
		{
			Touch currentTouch = Input.touches[i];

			if (currentTouch.fingerId == id)
			{
				return true;
			}
		}

		return false;
	}

	public TouchData GetTouchByFingerID(int id)
	{
		if (id < 0)
		{
			return new TouchData(MouseEventSystem.Instance.CurrentPointerData);
		}
		
		for (int i = 0; i < Input.touches.Length; i++)
		{
			Touch currentTouch = Input.touches[i];

			if (currentTouch.fingerId == id)
			{
				return new TouchData(currentTouch);
			}
		}

		return null;
	}

	public TouchData GetLastTouchData ()
	{
		return GetTouchByFingerID(LastFingerID);
	}

	protected virtual void Start()
	{
		AttachToEvents();
	}

	protected virtual void Update()
	{
		CheckMobileInput();
	}

	protected virtual void Awake()
	{
		Instance = this;
	}

	protected virtual void OnDestroy()
	{
		DetachFromEvents();
	}

	// EVENTS
	private void AttachToEvents()
	{
		if (Application.isMobilePlatform == true)
		{
			return;
		}

		MouseEventSystem.OnMouseDrag += HandleOnMouseDrag;
		MouseEventSystem.OnMouseDown += HandleOnMouseDown;
		MouseEventSystem.OnMouseUp += HandleOnMouseUp;
		MouseEventSystem.OnMouse += HandleOnMouse;
	}

	private void DetachFromEvents()
	{
		if (Application.isMobilePlatform == true)
		{
			return;
		}

		MouseEventSystem.OnMouseDrag -= HandleOnMouseDrag;
		MouseEventSystem.OnMouseDown -= HandleOnMouseDown;
		MouseEventSystem.OnMouseUp -= HandleOnMouseUp;
		MouseEventSystem.OnMouse -= HandleOnMouse;
	}

	// INPUT
	private void CheckMobileInput()
	{
		#if UNITY_ANDROID
		{
			if (TouchScreenKeyboard.visible == true)
			{
				return;
			}
		}
		#endif
		
		OnBeginUpdateTouch();

		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch currentTouch = Input.touches[i];

			switch (currentTouch.phase)
			{
				case TouchPhase.Began:
					LastFingerID = currentTouch.fingerId;

					OnTouchDown(new TouchData(currentTouch));
					break;

				case TouchPhase.Stationary:
					OnTouchHold(new TouchData(currentTouch));
					break;

				case TouchPhase.Moved:
					OnTouchMove(new TouchData(currentTouch));
					break;

				case TouchPhase.Canceled:
				case TouchPhase.Ended:
					OnTouchUp(new TouchData(currentTouch));
					break;
			}
		}
		
		OnEndUpdateTouch();
	}

	// EVENTS HANDLING
	private void HandleOnMouseDrag(MouseEventSystem.PointerData pointerData)
	{
		OnTouchMove(new TouchData(pointerData));
	}

	private void HandleOnMouseDown(MouseEventSystem.PointerData pointerData)
	{
		LastFingerID = pointerData.FingerID;

		OnTouchDown(new TouchData(pointerData));
	}

	private void HandleOnMouseUp(MouseEventSystem.PointerData pointerData)
	{
		OnTouchUp(new TouchData(pointerData));
	}

	private void HandleOnMouse(MouseEventSystem.PointerData pointerData)
	{
		OnTouchHold(new TouchData(pointerData));
	}

	#endregion

	#region CLASS_ENUMS

	public class TouchData
	{
		#region MEMBERS

		#endregion

		#region PROPERTIES

		public Vector2 Position { get; set; }
		public Vector2 DeltaPosition { get; set; }
		public float DeltaTime { get; set; }

		public int FingerID { get; set; }
		public int TapCount { get; set; }

		public TouchPhase Phase { get; set; }

		#endregion

		#region FUNCTIONS

		public TouchData()
		{

		}

		public TouchData(Touch source)
		{
			Position = source.position;
			DeltaPosition = source.deltaPosition;
			DeltaTime = source.deltaTime;

			FingerID = source.fingerId;
			TapCount = source.tapCount;

			Phase = source.phase;
		}

		public TouchData(MouseEventSystem.PointerData source)
		{
			Position = source.Position;
			DeltaPosition = source.Delta;
			DeltaTime = Time.unscaledDeltaTime;
			
			switch (source.Phase)
			{
				case MouseEventSystem.State.DOWN:
					Phase = TouchPhase.Began;
					break;

				case MouseEventSystem.State.HOLD:
					Phase = TouchPhase.Stationary;
					break;

				case MouseEventSystem.State.DRAG:
					Phase = TouchPhase.Moved;
					break;

				case MouseEventSystem.State.NONE:
				case MouseEventSystem.State.UP:
					Phase = TouchPhase.Ended;
					break;
			}
		}

		#endregion

		#region CLASS_ENUMS

		#endregion
	}

	#endregion
}