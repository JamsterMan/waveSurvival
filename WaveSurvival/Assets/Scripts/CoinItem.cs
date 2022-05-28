using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : Item
{
    private readonly ItemType itemType = ItemType.consumable;
    public int coinValue = 5;
    public Sprite sprite;

    public CoinItem(Sprite coin)
    {
        sprite = coin;
    }

    public void OnPickUp(GameObject player)
    {
        PlayerGold pGold = player.GetComponent<PlayerGold>();
        pGold.AddGold(coinValue);
    }

    public bool CanPlayerTakeItem(GameObject player)
    {
        return true;// can always pick up coins
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
