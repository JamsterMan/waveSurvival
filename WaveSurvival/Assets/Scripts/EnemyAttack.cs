using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask PlayerLayer;

    public int attackDamage = 5;
    public float attackRange = 0.5f;

    /*
     * checks if player is in range, then does damage
     */
    public virtual void Attack()
    {
        //play animations

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, PlayerLayer);

        foreach (Collider2D hitPlayer in hitPlayers)
        {
            hitPlayer.GetComponent<PlayerHealth>().DealDamage(attackDamage);
        }
    }

    //draws circle in editor to show attack range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
