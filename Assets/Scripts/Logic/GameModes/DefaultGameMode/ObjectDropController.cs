using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultGameModeControllers
{
	public class ObjectDropController : MonoBehaviour
	{
		#region MEMBERS

		#endregion

		#region PROPERTIES

		#endregion

		#region FUNCTIONS

		protected void Awake ()
		{
			AttachToEvents();
		}

		protected void OnDestroy ()
		{
			DetachFromEvents();
		}

		private void AttachToEvents ()
		{
			UIEventSystem.OnUIBehaviourDrop += HandleUIBehaviourDropEvent;
		}

		private void DetachFromEvents ()
		{
			UIEventSystem.OnUIBehaviourDrop -= HandleUIBehaviourDropEvent;
		}

		private void HandleUIBehaviourDropEvent(UIBehaviour target)
		{
		}

		#endregion

		#region CLASS_ENUMS

		#endregion
	}
}
