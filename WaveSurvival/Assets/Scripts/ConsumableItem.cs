using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    private ItemType itemType;

    public ConsumableItem(ItemType type)
    {
        itemType = type;
    }

    public void OnBuy()
    {
        //changes on what consuable is being used
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}
