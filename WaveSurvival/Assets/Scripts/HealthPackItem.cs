using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPackItem : Item
{
    private readonly ItemType itemType = ItemType.consumable;
    private readonly int healAmount = 2;
    public Sprite sprite;

    public HealthPackItem(Sprite hPack)
    {
        sprite = hPack;
        healAmount = 2;
    }

    public void OnPickUp(GameObject player)
    {
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (!ph.IsMaxHealth())
        {
            ph.HealHealth(healAmount);
        }
    }

    public bool CanPlayerTakeItem(GameObject player)
    {
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        return !ph.IsMaxHealth();
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

    public Sprite GetItemSprite()
    {
        return sprite;
    }
}
