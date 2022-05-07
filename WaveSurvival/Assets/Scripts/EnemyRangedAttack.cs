using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : EnemyAttack
{
    public GameObject rangedShot;

    public override void Attack()
    {
        //play animations

        Instantiate(rangedShot, attackPoint.position, attackPoint.rotation, this.transform);
    }
}
