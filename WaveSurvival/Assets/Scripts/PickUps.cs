using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    private Item item;
    public SpriteRenderer spriteRenderer;

    public Sprite healthPack;
    public Sprite coin;

    private void Start()
    {
        //choose which item here
        int itemId = Random.Range(0, 2);
        if (itemId == 0)
            item = new CoinItem(coin);
        else
            item = new HealthPackItem(healthPack);

        spriteRenderer.sprite = item.GetItemSprite();
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
