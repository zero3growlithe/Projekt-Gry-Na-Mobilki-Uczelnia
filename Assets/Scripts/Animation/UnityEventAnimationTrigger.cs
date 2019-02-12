using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventAnimationTrigger : MonoBehaviour
{
    public UnityEvent eventsToTrigger;

	public void TriggerUnityEvent ()
	{
		eventsToTrigger.Invoke();
	}
}
