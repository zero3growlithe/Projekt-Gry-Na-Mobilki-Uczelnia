using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class GameplayItem : MonoBehaviour
{
    public ItemTypes ItemType;

    //GameplayItem(ItemTypes item)
    //{
    //    this.ItemType = item;
    //}

    public enum ItemTypes
    {
    FISH = 0,
    DRUMSTICK = 4,
    MUSHROOMS = 1,
    LEEK = 5,
    BUGS = 2,
    APPLE = 6,
    SHOE = 3,
    CAN = 7
}
}
