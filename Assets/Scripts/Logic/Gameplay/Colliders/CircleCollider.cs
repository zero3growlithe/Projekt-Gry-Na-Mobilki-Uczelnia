using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : BaseCollider
{
	#region MEMBERS

	#endregion

	#region PROPERTIES


	#endregion

	#region FUNCTIONS

	protected override void Update ()
	{
		base.Update();
		
		CheckCollisions();
	}

	private void CheckCollisions()
	{
		if (CurrentSprite == null)
		{
			return;
		}

		
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
