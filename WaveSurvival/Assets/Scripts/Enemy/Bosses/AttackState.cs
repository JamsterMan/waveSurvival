using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Vector2 playerPos;

    public override void EnterState(BossState boss)
    {
        Debug.Log("Entered Attack State");
    }

    public override void UpdateState(BossState boss)
    {
        playerPos = boss.player.position;
    }

    public override void FixedUpdateState(BossState boss)
    {
        Vector2 lookDirection = playerPos - boss.rb.position;
        if (lookDirection.magnitude > boss.attackRange)//&& canJump)
        {
            boss.SwitchState(boss.chaseState);
        }
    }
}
