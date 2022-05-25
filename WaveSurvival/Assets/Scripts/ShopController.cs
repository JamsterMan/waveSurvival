using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { consumable, passiveItem };
public class ShopController : MonoBehaviour
{
    //public List<PassiveItem> itemList = new List<PassiveItem>(); //list for pasive items 
    public PassiveItem[] itemArray;

    //public List<Item> consumableItemList = new List<Item>();
    public HealthPackItem healthPack;

    public ShopItem[] shopItems;

    // Start is called before the first frame update
    void Start()
    {
        //get/set list of all items

        ShopRefresh();
    }

    /*
     *Sets every shop item to a corrisponding item
     */
    private void SetShopItem(ShopItem shopItem)
    {
        if (shopItem.GetShopItemType() == ItemType.consumable)
        {
            shopItem.SetShopItem(healthPack);
        }
        else
        {
            if (itemArray.Length > 0)
            {
                int itemIndex = Random.Range(0, itemArray.Length);
                shopItem.SetShopItem(itemArray[itemIndex]);
            }
            else//if there are no items to spawn
            {
                shopItem.SetShopItem(healthPack);
                Debug.Log("no items in list");
            }
        }
    }

    /*
     * sets all shop item pedestals to new items
     */
    public void ShopRefresh()
    {
        foreach (ShopItem item in shopItems)
        {
            SetShopItem(item);
        }
    }
}
