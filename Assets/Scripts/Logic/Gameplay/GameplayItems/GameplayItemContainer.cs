using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameplayItemContainer : MonoBehaviour
    {
    public ContainerTypes ContainerType;

    GameplayItemContainer(ContainerTypes type)
    {
        this.ContainerType = type;
    }

    public Boolean ItemMatchesContainter(GameplayItem item)
    {
        return ((int)this.ContainerType).Equals((int)item.ItemType % 4);
    }

    public enum ContainerTypes
    {
        CAT = 0,
        POT = 1,
        HEDGEHOG = 2,
        TRASH = 3,
    }
}
