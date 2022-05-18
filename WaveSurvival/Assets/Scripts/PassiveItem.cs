using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassiveItem : Item
{
    private readonly ItemType itemType = ItemType.passiveItem;

    public Sprite sprite;

    //stats the item could change
    public int attackDamage = 0;
    public float attackRange = 0f;
    public float attackRate = 0f;
    public float rangedAttackRate = 0f;
    public int maxBlastAmmo = 0;
    public int chargePerAmmo = 0;
    public int maxHealth = 0;
    public float moveSpeed;

    public bool CanPlayerTakeItem(GameObject player)
    {
        return true; //player can always take passive items??
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

    public void OnBuy(GameObject player)
    {
        PlayerAttack pA= player.GetComponent<PlayerAttack>();
        if(attackDamage != 0)
        {
            pA.ChangePlayerDamage(attackDamage);
        }
    }

    public Sprite GetItemSprite()
    {
        return sprite;
    }
}
