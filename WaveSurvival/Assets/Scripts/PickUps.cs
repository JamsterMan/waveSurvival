using UnityEngine;

public class PickUps : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite healthPack;
    public Sprite coin;

    private Item _item;

    private void Start()
    {
        //choose which item here
        int itemId = Random.Range(0, 2);
        if (itemId == 0)
            _item = new CoinItem(coin);
        else
            _item = new HealthPackItem(healthPack);

        spriteRenderer.sprite = _item.GetItemSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            if (_item.CanPlayerTakeItem(player))
            {
                _item.OnPickUp(player);

                Destroy(this.gameObject);
            }
        }
    }
}
