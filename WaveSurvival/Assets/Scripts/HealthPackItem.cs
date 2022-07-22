using UnityEngine;

[System.Serializable]
public class HealthPackItem : Item
{
    public Sprite sprite;

    private readonly ItemType _itemType = ItemType.consumable;
    private readonly int _healAmount = 2;

    public HealthPackItem(Sprite hPack)
    {
        sprite = hPack;
        _healAmount = 2;
    }

    public void OnPickUp(GameObject player)
    {
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (!ph.IsMaxHealth())
        {
            ph.HealHealth(_healAmount);
        }
    }

    public bool CanPlayerTakeItem(GameObject player)
    {
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        return !ph.IsMaxHealth();
    }

    public ItemType GetItemType()
    {
        return _itemType;
    }

    public Sprite GetItemSprite()
    {
        return sprite;
    }
}
