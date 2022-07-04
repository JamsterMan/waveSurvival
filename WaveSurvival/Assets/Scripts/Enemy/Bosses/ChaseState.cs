using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    protected Vector2 movement;
    protected Vector2 playerPos;

    public override void EnterState(BossState boss)
    {
        Debug.Log("Entered Chase State");
    }

    public override void UpdateState(BossState boss)
    {

    }

    public override void FixedUpdateState(BossState boss)
    {
        Vector2 lookDirection = playerPos - boss.rb.position;

        movement = lookDirection / lookDirection.magnitude;
        boss.rb.MovePosition(boss.rb.position + movement * boss.moveSpeed * Time.fixedDeltaTime);

        if (lookDirection.magnitude <= boss.attackRange)//&& canJump)
        {
            //boss.SwitchState(boss.attackState);
        }
    }
}
