using UnityEngine;

public interface Item
{
    //what to do when item is bought
    void OnPickUp(GameObject player);

    //get the type of the item
    ItemType GetItemType();

    //returns true if it is possible 
    bool CanPlayerTakeItem(GameObject player);

    //returns the sprite of the item
    Sprite GetItemSprite();
}
