using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Item
{
    //what to do when item is bought
    void OnBuy(GameObject player);

    //get the type of the item
    ItemType GetItemType();

    //returns true if it is possible 
    bool CanPlayerTakeItem(GameObject player);
}
