using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class UIInputFieldEvents : UIEvents
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	// VARIABLES
	private InputField TargetInputField {get; set;}

	#endregion

	#region FUNCTIONS

	public override void OnPointerDown (PointerEventData data)
	{
		base.OnPointerDown(data);

		UIEventSystem.Instance.NotifyOnUIInputFieldFocus(TargetInputField);
	}
	
	// workaround for Unity bug
	public override void OnPointerUp (TouchEventSystem.TouchData pointerData)
	{
		base.OnPointerUp(pointerData);

		UIEventSystem.Instance.NotifyOnUIInputFieldDefocus(TargetInputField);
	}

	public override void OnPointerUp (PointerEventData data)
	{
		base.OnPointerUp(data);

		if (PointerUpHandlerType != EventHandler.CUSTOM_UP)
		{
			return;
		}

		UIEventSystem.Instance.NotifyOnUIInputFieldDefocus(TargetInputField);
	}

	protected override void Awake ()
	{
		base.Awake();

		TargetInputField = GetComponent<InputField>();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
	

}
