using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseOnClick : MonoBehaviour
{
	#region MEMBERS

	[SerializeField]
	private Camera targetCamera;
	[SerializeField]
	private int brushSizeInPixels = 30;

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS

	protected void Update ()
	{
		CheckMouseInput();
	}

	private void CheckMouseInput()
	{
		if (Input.GetMouseButton(0) == false)
		{
			return;
		}
		
		Vector3 mousePosition = Input.mousePosition;
		Vector3 coords = targetCamera.ScreenToWorldPoint(mousePosition);
		DestructibleSprite sprite = DestructibleSpriteManager.Instance.GetSpriteAtPoint(coords);

		if (sprite == null)
		{
			Debug.Log("Nothing clicked yo");

			return;
		}

		coords.z = sprite.transform.position.z;

		Vector2 pointOffset = sprite.transform.InverseTransformPoint(coords);
		Vector2 pixel = new Vector2(
			pointOffset.x / sprite.SpriteBounds.size.x * sprite.SpriteRect.width,
			pointOffset.y / sprite.SpriteBounds.size.y * sprite.SpriteRect.height);

		sprite.TargetTexture.SetPixel((int)pixel.x, (int)pixel.y, Color.clear);
		sprite.TargetTexture.Apply();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
