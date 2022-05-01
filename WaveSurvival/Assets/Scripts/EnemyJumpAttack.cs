using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpAttack : EnemyAttack
{
    public EnemyJumpMovement movement;
    public float jumpRange = 5f;

    public void Jump()
    {
        movement.EnemyJump();
    }
}
