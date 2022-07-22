using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlastEnemy : EnergyBlast
{
    private void Start()
    {
        _startPos = transform.position;
        rb.velocity = transform.up * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ph.IsDamageable())
            {
                collision.gameObject.GetComponent<PlayerHealth>().DealDamage(blastDamage);
                Destroy(this.gameObject);
            }
        }
    }
}
