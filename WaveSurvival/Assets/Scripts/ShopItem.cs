using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int consumableItemCost = 5;
    public int passiveItemCost = 15;
    private int itemCost = 5;
    public SpriteRenderer shopItemSprite;
    public SpriteRenderer shopItemPriceSprite;
    public Sprite consumablePriceSprite;
    public Sprite passivePriceSprite;

    public Transform playerPos;
    public float safeDistance = 2f;
    [SerializeField] private bool canBeUsed = true;

    public ItemType shopItemType = ItemType.consumable;
    public Item shopItem;

    private void Start()
    {
        if (shopItemType == ItemType.consumable)
        {
            itemCost = consumableItemCost;
            shopItemPriceSprite.sprite = consumablePriceSprite;
        }
        else
        {
            itemCost = passiveItemCost;
            shopItemPriceSprite.sprite = passivePriceSprite;
        }
    }

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
                shopItem.OnBuy(player);
                pg.RemoveGold(itemCost);
                canBeUsed = false;

                Debug.Log("Item Bought.");//remove item here
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

        Debug.Log("Item set");
    }

}
