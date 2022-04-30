using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public Animator animator;

    public int attackDamage = 5;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public GameObject energyBlast;
    public float rangedAttackRate = 3f;
    float nextRangedAttackTime = 0f;//
    public int maxBlastAmmo = 3;
    private int blastAmmo;
    public int chargePerAmmo = 20;
    private int ammoCharge = 0;//when this = chargePerAmmo, increase ammo by one
    public Transform firePoint;

    private void Start()
    {
        blastAmmo = maxBlastAmmo;
        Debug.Log("Ammo: " + blastAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if (Time.time > nextRangedAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //check if ammo is not 0
                if (blastAmmo > 0)
                {
                    RangedAttack();
                    nextAttackTime = Time.time + 1f / rangedAttackRate;
                }
                Debug.Log("Ammo: " + blastAmmo);
            }
        }
    }

    void Attack()
    {
        //play animations
        animator.SetBool("Attack", true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D hitEnemy in hitEnemies)
        {
            hitEnemy.GetComponent<Enemy>().DealDamage(attackDamage);
            //add to next ammo charge
            if (blastAmmo < maxBlastAmmo)
            {
                ammoCharge += attackDamage;
                if (ammoCharge >= chargePerAmmo)
                {
                    ammoCharge = 0;
                    blastAmmo++;
                    Debug.Log("Ammo: " + blastAmmo);
                }
            }
            
        }
    }
    void RangedAttack()
    {
        //play animations
        //animator.SetBool("Attack", true);
        
        GameObject blast = Instantiate(energyBlast, firePoint.position, firePoint.rotation, this.transform);
        //decrease ammo
        blastAmmo--;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
