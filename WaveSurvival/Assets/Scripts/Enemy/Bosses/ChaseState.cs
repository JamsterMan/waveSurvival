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
        playerPos = boss.player.position;
    }

    public override void FixedUpdateState(BossState boss)
    {
        Vector2 lookDirection = playerPos - boss.rb.position;
        float speed = boss.moveSpeed;
        movement = lookDirection / lookDirection.magnitude;

        if (boss.phase2)
            speed = boss.p2MoveSpeed;

        boss.rb.MovePosition(boss.rb.position + movement * speed * Time.fixedDeltaTime);

        if (lookDirection.magnitude <= boss.attackRange)//&& canJump)
        {
            boss.SwitchState(boss.attackState);
        }
    }
}
