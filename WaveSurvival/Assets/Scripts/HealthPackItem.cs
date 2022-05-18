using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPackItem : Item
{
    private readonly ItemType itemType = ItemType.consumable;
    public int healAmount = 5;
    public Sprite sprite;

    public HealthPackItem(ItemType type)
    {
        itemType = type;
    }

    public void OnBuy(GameObject player)
    {
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (!ph.IsMaxHealth())
        {
            Debug.Log("Player Healed");
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
