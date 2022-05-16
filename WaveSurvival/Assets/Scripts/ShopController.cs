using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { consumable, passiveItem };
public class ShopController : MonoBehaviour
{
    //public List<Item> itemList = new List<Item>(); //list for pasive items 

    public List<ConsumableItem> itemList = new List<ConsumableItem>();

    // Start is called before the first frame update
    void Start()
    {
        //get/set list of all items
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
