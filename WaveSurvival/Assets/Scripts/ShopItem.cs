using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int itemCost = 5;

    public Transform playerPos;
    public float safeDistance = 2f;
    [SerializeField] private bool canBeUsed = true;

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
        if (other.gameObject.CompareTag("Player") && canBeUsed)//only start the wave if the player hits the button
        {
            PlayerGold pg = other.gameObject.GetComponent<PlayerGold>();
            PlayerHealth ph = other.gameObject.GetComponent<PlayerHealth>();
            if (!ph.IsMaxHealth() && pg.HasEnoughGold(itemCost)) {
                if (HealItem(other.gameObject))//health pack specific
                {
                    pg.RemoveGold(itemCost);
                    canBeUsed = false;
                }
            }
        }
    }

    /**************************************************
     * above is shop item generalized (all items would need)
     * 
     * Below is health pack specific
     */

    public int healAmount = 5;

    /*
     * returns true if player could be healed, else false
     */
    private bool HealItem(GameObject player)
    {
        PlayerHealth ph = player.gameObject.GetComponent<PlayerHealth>();
        if (!ph.IsMaxHealth())
        {
            Debug.Log("Item Bought");
            ph.HealHealth(healAmount);
            return true;
        }
        else
        {
            return false;
        }
    }

}
