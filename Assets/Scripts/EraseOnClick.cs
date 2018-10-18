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
	[SerializeField]
	private Color32 brushColor = Color.clear;

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
		
		Color32[] pixels = sprite.TargetTexturePixels;
		
		PaintCircleInArray(ref pixels, sprite.TargetTexture.width, (int)pixel.x, (int)pixel.y, brushSizeInPixels, brushColor);

		sprite.TargetTexture.SetPixels32(pixels);
		sprite.TargetTexture.Apply();

		sprite.TargetTexturePixels = pixels;
	}

	public void PaintCircleInArray(ref Color32[] colorsArray, int arrayWidth, int xCenter, int yCenter, int radius, Color32 color)
	{
		int i, x, y = 0;
		int sqrRadius = radius * radius;
		
		for (i = 1; i <= radius * 2; i++)
		{
			SetPixelInImage(ref colorsArray, arrayWidth, xCenter, yCenter - radius + i, color);
		}

		for (x = 1; x <= radius; x++)
		{
			y = (int)(Math.Sqrt(sqrRadius - x * x) + 0.5f);

			for (i = 1; i <= y * 2; i++)
			{
				SetPixelInImage(ref colorsArray, arrayWidth, xCenter + x, yCenter - y + i, color);
				SetPixelInImage(ref colorsArray, arrayWidth, xCenter - x, yCenter - y + i, color);
			}
		}
	}

	private void SetPixelInImage (ref Color32[] array, int width, int x, int y, Color32 color)
	{
		int index = y * width + x;		

		if (index >= 0 && index < array.Length)
		{
			array[index] = color;
		}
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
