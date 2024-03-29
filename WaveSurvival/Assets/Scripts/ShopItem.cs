﻿using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int consumableItemCost = 5;
    public int passiveItemCost = 15;
    public SpriteRenderer shopItemSprite;
    public SpriteRenderer shopItemPriceSprite;
    public Sprite consumablePriceSprite;
    public Sprite passivePriceSprite;

    //public Transform playerPos;
    public float safeDistance = 2f;

    public ItemType shopItemType = ItemType.consumable;
    public Item shopItem;

    private int _itemCost = 5;
    [SerializeField] private bool _canBeUsed = true;

    private void Start()
    {
        if (shopItemType == ItemType.consumable)
        {
            _itemCost = consumableItemCost;
            shopItemPriceSprite.sprite = consumablePriceSprite;
        }
        else
        {
            _itemCost = passiveItemCost;
            shopItemPriceSprite.sprite = passivePriceSprite;
        }
    }

    /*private void Update()
    {
        if (!_canBeUsed)
        {
            if ((transform.position - playerPos.position).magnitude >= safeDistance)
                _canBeUsed = true;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && _canBeUsed && shopItem != null)//only start the wave if the player hits the button
        {
            GameObject player = other.gameObject;
            PlayerGold pg = player.GetComponent<PlayerGold>();
            if (pg.HasEnoughGold(_itemCost) && shopItem.CanPlayerTakeItem(player)) {
                shopItem.OnPickUp(player);
                pg.RemoveGold(_itemCost);
                _canBeUsed = false;

                //Debug.Log("Item Bought.");//remove item here
                gameObject.SetActive(false);
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
        shopItemSprite.sprite = item.GetItemSprite();
        //set item active
        gameObject.SetActive(true);
    }

}
