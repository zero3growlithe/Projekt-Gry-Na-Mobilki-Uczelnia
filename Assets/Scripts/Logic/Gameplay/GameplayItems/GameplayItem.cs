using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class GameplayItem : DragableUIElement
{
	public ItemTypes ItemType;

	public enum ItemTypes
	{
		FISH,
		MEAT,
		MUSHROOMS,
		LEEK,
		WORMS,
		APPLE,
		SHOE,
		CAN
	}
}
