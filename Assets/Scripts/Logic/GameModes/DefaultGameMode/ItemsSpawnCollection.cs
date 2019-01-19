using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsSpawnCollection
{
	#region MEMBERS

	[SerializeField]
	private GameObject[] items;

	#endregion

	#region PROPERTIES

	public GameObject[] Items {
		get {return items;}
	}

	#endregion

	#region FUNCTIONS

	#endregion

	#region CLASS_ENUMS

	#endregion
}
