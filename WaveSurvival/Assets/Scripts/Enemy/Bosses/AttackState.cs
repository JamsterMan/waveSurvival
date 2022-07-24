using UnityEngine;

public class AttackState : State
{
    Vector2 _lookDirection;
    public override void EnterState(BossState boss)
    {
        //Debug.Log("Entered Attack State");
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

        if (boss.playerH.IsDamageable())
            Attack(boss);

    }

    private void Attack(BossState boss)
    {
        //play animations

        Collider2D[] hitPlayers = Physics2D.OverlapBoxAll(boss.attackPoint.position, boss.attackPointSize, 0, boss.PlayerLayer);//0 -> angle

        if (hitPlayers == null)
            return;

        int damage = boss.attackDamage;
        if (boss.phase2)
            damage = boss.p2AttackDamage;

        foreach (Collider2D hitPlayer in hitPlayers)
        {
            if (hitPlayer != null)
                hitPlayer.GetComponent<PlayerHealth>().DealDamage(damage);
        }
    }

    public override void CheckStateSwitch(BossState boss)
    {

        if (_lookDirection.magnitude > boss.attackRange)
        {
            boss.SwitchState(boss.chaseState);
        }
    }
}
