using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollider : MonoBehaviour
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	// CACHES
	protected Transform TargetTransform { get; private set; }
	protected DestructibleSprite CurrentSprite {get; private set;}

	// VARIABLES
	protected Vector3 CurrentVelocity { get; private set; }
	protected Vector3 LastTransformPosition { get; private set; }

	#endregion

	#region FUNCTIONS

	protected virtual void Awake()
	{
		TargetTransform = transform;
	}

	protected virtual void Update ()
	{
		UpdateCurrentBackgroundSprite();
		CheckVelocity();
	}

	private void CheckVelocity()
	{
		CurrentVelocity = (TargetTransform.position - LastTransformPosition) / Time.deltaTime;

		LastTransformPosition = TargetTransform.position;
	}

	private void UpdateCurrentBackgroundSprite()
	{
		CurrentSprite = DestructibleSpriteManager.Instance.GetSpriteAtPoint(TargetTransform.position);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
