using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int itemCost = 5;
    public SpriteRenderer spriteRenderer;

    public Transform playerPos;
    public float safeDistance = 2f;
    [SerializeField] private bool canBeUsed = true;

    public ItemType shopItemType = ItemType.consumable;
    public Item shopItem;

    private void Update()
    {
        if (!canBeUsed)
        {
            if ((transform.position - playerPos.position).magnitude >= safeDistance)
                canBeUsed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && canBeUsed && shopItem != null)//only start the wave if the player hits the button
        {
            GameObject player = other.gameObject;
            PlayerGold pg = player.GetComponent<PlayerGold>();
            if (pg.HasEnoughGold(itemCost) && shopItem.CanPlayerTakeItem(player)) {
                Debug.Log("Item Bought.");
                shopItem.OnBuy(player);
                pg.RemoveGold(itemCost);
                canBeUsed = false;
            }
        }
    }

    public ItemType GetShopItemType()
    {
        return shopItemType;
    }

    public void SetShopItem(Item item)
    {
        shopItem = item;
        //set Sprite
        spriteRenderer.sprite = item.GetItemSprite();

        Debug.Log("Item set");
    }

    /**************************************************
     * above is shop item generalized (all items would need)
     * 
     * Below is health pack specific
     *

    public int healAmount = 5;

    /*
     * returns true if player could be healed, else false
     *
    private bool HealItem(GameObject player)
    {
        PlayerHealth ph = player.gameObject.GetComponent<PlayerHealth>();
        if (!ph.IsMaxHealth())
        {
            Debug.Log("Item Bought");
            ph.HealHealth(healAmount);
            return true;
        }
        else
        {
            return false;
        }
    }*/

}
