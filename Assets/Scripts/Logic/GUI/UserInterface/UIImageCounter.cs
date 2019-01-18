using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIImageCounter : MonoBehaviour
{
	#region MEMBERS

	[SerializeField]	
	private GameObject[] targetImages;

	#endregion

	#region PROPERTIES

	private GameObject[] TargetImages {
		get {return targetImages;}
	}

	#endregion

	#region FUNCTIONS

	public void SetActiveByCount (int count)
	{
		for (int i = 0; i < TargetImages.Length; i++)
		{
			TargetImages[i].SetActiveOptimized(i < count);
		}
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
