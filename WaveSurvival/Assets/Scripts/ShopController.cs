using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { consumable, passiveItem };
public class ShopController : MonoBehaviour
{
    //public List<PassiveItem> itemList = new List<PassiveItem>(); //list for pasive items 

    //public List<Item> consumableItemList = new List<Item>();
    public Item healthPack;
    public HealthPackItem testPack;

    public ShopItem shopItem;

    // Start is called before the first frame update
    void Start()
    {
        healthPack = new HealthPackItem(ItemType.consumable);
        //get/set list of all items
        if(shopItem.GetShopItemType() == ItemType.consumable)
        {
            shopItem.SetShopItem(testPack);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
