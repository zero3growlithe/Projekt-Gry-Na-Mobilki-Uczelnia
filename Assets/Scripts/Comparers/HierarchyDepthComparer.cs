using UnityEngine;
using System;
using System.Collections.Generic;

public class HierarchyDepthComparer: IComparer<Transform>
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS
	
	public int Compare (Transform x, Transform y)
	{
		if (x.IsCloserInHierarchyThan(y) == true)
		{
			return -1;
		}

		return 1;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}