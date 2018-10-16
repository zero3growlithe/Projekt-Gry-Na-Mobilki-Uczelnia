using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleSpriteManager : MonoBehaviourSingleton<DestructibleSpriteManager>
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	private List<DestructibleSprite> destructibleSprites = new List<DestructibleSprite>();

	#endregion

	#region FUNCTIONS

	public void Subscribe (DestructibleSprite target)
	{
		if (destructibleSprites.Contains(target) == false)
		{
			destructibleSprites.Add(target);
		}
	}

	public void Unsubscribe (DestructibleSprite target)
	{
		if (destructibleSprites.Contains(target) == true)
		{
			destructibleSprites.Remove(target);
		}
	}

	public DestructibleSprite GetSpriteAtPoint (Vector3 point)
	{
		for (int i = 0; i < destructibleSprites.Count; i++)
		{
			if (point.IsInBounds2D(destructibleSprites[i].SpriteBounds))
			{
				return destructibleSprites[i];
			}
		}

		return null;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
