using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private Vector2 playerPos;

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

        if (boss.playerH.IsDamageable())
            Attack(boss);

        if (lookDirection.magnitude > boss.attackRange)
        {
            boss.SwitchState(boss.chaseState);
        }
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
}
