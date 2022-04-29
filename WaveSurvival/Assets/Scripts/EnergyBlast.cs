using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlast : MonoBehaviour
{
    public Rigidbody2D rb;
    public float projectileSpeed = 10f;
    public int blastDamage = 10;
    public float attackRange = 10f;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
        rb.velocity = transform.up * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //check if projectile traveled too far then destroy  
        Vector2 distance = new Vector2(transform.position.x, transform.position.y) - startPos;
        if(distance.magnitude > attackRange)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().DealDamage(blastDamage);
            Destroy(this.gameObject);
        }
    }
}
