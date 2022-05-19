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

    public ShopItem shopItem;
    public ShopItem shopItem2;

    // Start is called before the first frame update
    void Start()
    {
        //get/set list of all items
        if(shopItem.GetShopItemType() == ItemType.consumable)
        {
            shopItem.SetShopItem(healthPack);
        }

        int itemIndex = Random.Range(0, itemArray.Length);
        //shopItem.SetShopItem(item);
        if(itemArray.Length > 0)
            shopItem2.SetShopItem(itemArray[itemIndex]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
