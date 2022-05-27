using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    private Item item;

    private void Start()
    {
        //choose which item here

        item = new HealthPackItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            if (item.CanPlayerTakeItem(player))
            {
                item.OnPickUp(player);

                Destroy(this.gameObject);
            }
        }
    }
}
