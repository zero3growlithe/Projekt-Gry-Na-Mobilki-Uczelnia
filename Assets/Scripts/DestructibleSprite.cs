using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructibleSprite : MonoBehaviour
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	public Bounds SpriteBounds {get; private set;}
	public SpriteRenderer TargetRenderer {get; private set;}
	public Texture2D TargetTexture {get; private set;}
	public Sprite TargetSprite {get; private set;}
	public Rect SpriteRect {get; private set;}

	#endregion

	#region FUNCTIONS

	protected void Start ()
	{
		TargetRenderer = gameObject.GetComponent<SpriteRenderer>();

		Sprite source = TargetRenderer.sprite;

		TargetTexture = new Texture2D(source.texture.width, source.texture.height, TextureFormat.ARGB32, false);
		TargetTexture.SetPixels32(source.texture.GetPixels32());
		TargetTexture.Apply();

		TargetSprite = Sprite.Create(TargetTexture, source.rect, source.pivot, source.pixelsPerUnit, 1, SpriteMeshType.FullRect, source.border, false);
		TargetRenderer.sprite = TargetSprite;
		
		SpriteRect = TargetRenderer.sprite.rect;
		SpriteBounds = TargetRenderer.sprite.bounds;
	}

	protected void OnEnable ()
	{
		DestructibleSpriteManager.Instance.Subscribe(this);
	}

	protected void OnDisable ()
	{
		DestructibleSpriteManager.Instance.Unsubscribe(this);
	}

	protected void OnDestroy ()
	{
		Destroy(TargetSprite);
		Destroy(TargetTexture);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
