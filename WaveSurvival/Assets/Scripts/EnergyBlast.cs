using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlast : MonoBehaviour
{
    public Rigidbody2D rb;
    public float projectileSpeed = 10f;
    public int blastDamage = 10;
    public float attackRange = 10f;
    public Animator animator;
    private bool canDamage = true;

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
            animator.SetBool("BlastEnd", true);
            canDamage = false;
            rb.velocity = new Vector2(0,0);//stop movement
            //Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canDamage)
        {
            collision.gameObject.GetComponent<Enemy>().DealDamage(blastDamage);

            animator.SetBool("BlastEnd", true);
            canDamage = false;
            //Destroy(this.gameObject);
        }
    }

    public void EnergyBlastEnd()
    {
        Destroy(this.gameObject);
    }
}
