using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[RequireComponent(typeof(CanvasRenderer))]
public class UIInteractablePanel : MonoBehaviour
{
	#region MEMBERS

	[Header("[ Base settings ]")]
	[SerializeField]
	private TouchGesturesEventConsumption touchGesturesConsumption;

	#endregion

	#region PROPERTIES

	public TouchGesturesEventConsumption TouchGesturesConsumption {
		get { return touchGesturesConsumption; }
	}

	// CACHES
	protected RectTransform CurrentRectTransform { get; private set; }
	protected CanvasRenderer CurrentRenderer { get; private set; }
	protected Canvas ParentCanvas { get; private set; }

	#endregion

	#region FUNCTIONS

	public bool ContainsSelectable (Selectable target)
	{
		if (target == null)
		{
			return false;
		}

		return target.transform.IsChildOf(transform);
	}

	public bool IsPositionOverRect (Vector2 position, Camera targetCamera = null)
	{
		CacheReferences();

		if (gameObject.activeInHierarchy == false || CurrentRectTransform == null)
		{
			return false;
		}

		if (targetCamera == null && ParentCanvas != null)
		{
			targetCamera = (ParentCanvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : ParentCanvas.worldCamera;
		}

		return RectTransformUtility.RectangleContainsScreenPoint(CurrentRectTransform, position, targetCamera);
	}

	public bool CheckIfClicked (MouseEventSystem.PointerData pointerData)
	{
		return IsPositionOverRect(pointerData.Position);
	}

	public bool CheckIfClicked (TouchEventSystem.TouchData touchData)
	{
		return IsPositionOverRect(touchData.Position);
	}

	protected virtual void Start ()
	{
		UIEventSystem.Instance.AddInteractablePanel(this);
	}

	protected virtual void Awake ()
	{
		CacheReferences();
	}

	protected virtual void Update ()
	{

	}

	protected virtual void OnEnable ()
	{

	}

	protected virtual void OnDisable ()
	{

	}

	protected virtual void OnDestroy ()
	{
		UIEventSystem.Instance.RemoveInteractablePanel(this);
	}

	private void CacheReferences ()
	{
		if (CurrentRectTransform == null)
		{
			CurrentRectTransform = GetComponent<RectTransform>();
		}

		if (CurrentRenderer == null)
		{
			CurrentRenderer = GetComponent<CanvasRenderer>();
		}

		if (ParentCanvas == null)
		{
			ParentCanvas = GetComponentInParent<Canvas>();
		}
	}

	#endregion

	#region CLASS_ENUMS

	[Serializable]
	public class TouchGesturesEventConsumption
	{
		#region MEMBERS

		[SerializeField]
		private bool shouldConsumePress = true;

		[SerializeField]
		private bool shouldConsumeDoubleClick = true;

		[SerializeField]
		private bool shouldConsumeSwipe = true;

		[SerializeField]
		private bool shouldConsumeDrag = true;

		[SerializeField]
		private bool shouldConsumePinch = true;


		#endregion

		#region PROPERTIES

		public bool ShouldConsumePress {
			get { return shouldConsumePress; }
		}

		public bool ShouldConsumeDoubleClick {
			get { return shouldConsumeDoubleClick; }
		}

		public bool ShouldConsumeSwipe {
			get { return shouldConsumeSwipe; }
		}

		public bool ShouldConsumeDrag {
			get { return shouldConsumeDrag; }
		}

		public bool ShouldConsumePinch {
			get { return shouldConsumePinch; }
		}

		#endregion

		#region FUNCTIONS

		#endregion
	}

	#endregion

}
