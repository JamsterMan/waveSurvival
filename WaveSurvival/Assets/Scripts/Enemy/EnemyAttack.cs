using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPoint;
    public Vector2 attackPointSize;
    public LayerMask PlayerLayer;

    public int attackDamage = 1;
    public float attackRange = 0.5f;

    /*
     * checks if player is in range, then does damage
     */
    public virtual void Attack()
    {
        //play animations

        Collider2D[] hitPlayers = Physics2D.OverlapBoxAll(attackPoint.position, attackPointSize, 0, PlayerLayer);//0 -> angle

        if (hitPlayers == null)
            return;

        foreach (Collider2D hitPlayer in hitPlayers)
        {
            if (hitPlayer != null)
                hitPlayer.GetComponent<PlayerHealth>().DealDamage(attackDamage);
        }
    }

    //draws circle in editor to show attack range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireCube(attackPoint.position,attackPointSize);
    }
}
