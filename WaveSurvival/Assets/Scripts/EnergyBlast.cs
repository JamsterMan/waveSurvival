using UnityEngine;

public class EnergyBlast : MonoBehaviour
{
    public Rigidbody2D rb;
    public float projectileSpeed = 10f;
    public int blastDamage = 10;
    public float attackRange = 10f;
    public Animator animator;

    private bool _canDamage = true;
    protected Vector2 _startPos;

    private void Start()
    {
        _startPos = transform.position;
        rb.velocity = transform.right * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //check if projectile traveled too far then destroy  
        Vector2 distance = new Vector2(transform.position.x, transform.position.y) - _startPos;
        if(distance.magnitude > attackRange)
        {
            animator.SetBool("BlastEnd", true);
            _canDamage = false;
            rb.velocity = new Vector2(0,0);//stop movement
            //Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _canDamage)
        {
            collision.gameObject.GetComponent<Enemy>().DealDamage(blastDamage);

            animator.SetBool("BlastEnd", true);
            _canDamage = false;
            //Destroy(this.gameObject);
        }
    }

    public void EnergyBlastEnd()
    {
        Destroy(this.gameObject);
    }
}
