using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : EnemyAttack
{
    public GameObject rangedShot;
    public Transform contactCentrePoint;

    public override void Attack()
    {
        //play animations

        Instantiate(rangedShot, attackPoint.position, attackPoint.rotation, this.transform);
    }


    public void ContactAttack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapBoxAll(contactCentrePoint.position, attackPointSize, 0, PlayerLayer);//0 -> angle

        if (hitPlayers == null)
            return;

        foreach (Collider2D hitPlayer in hitPlayers)
        {
            if (hitPlayer != null)
                hitPlayer.GetComponent<PlayerHealth>().DealDamage(attackDamage);
        }
    }
}
