using UnityEngine;
using System;
using System.Collections.Generic;

public class UIInteractablePanelDepthComparer: IComparer<UIInteractablePanel>
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS
	
	public int Compare (UIInteractablePanel x, UIInteractablePanel y)
	{
		Transform xTransform = x.transform;
		Transform yTransform = y.transform;

		if (xTransform.IsCloserInHierarchyThan(yTransform) == true)
		{
			return -1;
		}

		return 1;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}