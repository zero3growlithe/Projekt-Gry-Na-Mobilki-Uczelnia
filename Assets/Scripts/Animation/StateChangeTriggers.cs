using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateChangeTriggers : MonoBehaviour
{
    public UnityEvent OnEnableEvent;
    public UnityEvent OnDisableEvent;

	protected void OnEnable ()
	{
		OnEnableEvent.Invoke();
	}

	protected void OnDisable ()
	{
		OnDisableEvent.Invoke();
	}
}
