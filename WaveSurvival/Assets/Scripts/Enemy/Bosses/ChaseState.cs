using UnityEngine;

public class ChaseState : State
{
    private Vector2 _movement;
    private Vector2 _lookDirection;

    public override void EnterState(BossState boss)
    {
        Debug.Log("Entered Chase State");
        boss.animator.SetBool("EnemyMove", true);
    }

    public override void UpdateState(BossState boss)
    {
        //playerPos = boss.player.position;
        CheckStateSwitch(boss);
    }

    public override void FixedUpdateState(BossState boss)
    {
        _lookDirection = boss.playerPos - boss.rb.position;
        boss.LookAtPlayer(_lookDirection.x);
        float speed = boss.moveSpeed;
        _movement = _lookDirection / _lookDirection.magnitude;

        if (boss.phase2)
            speed = boss.p2MoveSpeed;

        boss.rb.MovePosition(boss.rb.position + _movement * speed * Time.fixedDeltaTime);
    }

    public override void CheckStateSwitch(BossState boss)
    {
        if (_lookDirection.magnitude > boss.minRangedAttackRange && Random.Range(0, 101) == 1)
        {
            //boss.SwitchState(boss.rangedAttackState);
            //boss.SwitchState(boss.jumpAttackState);
            boss.SwitchState(boss.rangedAttack2State);
        }

        if (_lookDirection.magnitude <= boss.attackRange)
        {
            boss.SwitchState(boss.attackState);
        }
    }
}
