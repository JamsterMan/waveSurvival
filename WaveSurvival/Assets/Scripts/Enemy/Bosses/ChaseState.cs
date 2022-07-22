using UnityEngine;

public class ChaseState : State
{
    private Vector2 _movement;

    public override void EnterState(BossState boss)
    {
        //Debug.Log("Entered Chase State");
        boss.animator.SetBool("EnemyMove", true);
    }

    public override void UpdateState(BossState boss)
    {
        //playerPos = boss.player.position;
    }

    public override void FixedUpdateState(BossState boss)
    {
        Vector2 lookDirection = boss.playerPos - boss.rb.position;
        boss.LookAtPlayer(lookDirection.x);
        float speed = boss.moveSpeed;
        _movement = lookDirection / lookDirection.magnitude;

        if (boss.phase2)
            speed = boss.p2MoveSpeed;

        boss.rb.MovePosition(boss.rb.position + _movement * speed * Time.fixedDeltaTime);

        if (lookDirection.magnitude > boss.minRangedAttackRange && Random.Range(0,81) == 1)
        {
            boss.SwitchState(boss.rangedAttackState);
        }

        if (lookDirection.magnitude <= boss.attackRange)
        {
            boss.SwitchState(boss.attackState);
        }
    }
}
