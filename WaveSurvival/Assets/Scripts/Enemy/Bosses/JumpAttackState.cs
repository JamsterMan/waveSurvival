using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackState : State
{
    Vector2 lookDirection;
    public override void EnterState(BossState boss)
    {
        Debug.Log("Entering Jump State");
        boss.EnemyJump();//corutine outside of state
    }

    public override void UpdateState(BossState boss)
    {
        CheckStateSwitch(boss);
    }

    public override void FixedUpdateState(BossState boss)
    {
        if (boss.isJumping)
        {
            lookDirection = boss.jumpLocation - boss.rb.position;

            if (lookDirection.magnitude != 0)
                boss.movement = lookDirection / lookDirection.magnitude;

            boss.rb.MovePosition(boss.rb.position + (boss.movement * boss.jumpSpeed * Time.fixedDeltaTime));

            if (boss.playerH.IsDamageable())
                Attack(boss);
        }
            
    }

    public override void CheckStateSwitch(BossState boss)
    {
        if (IsJumpDone(lookDirection, boss) )// && boss.isJumping)
        {
            boss.isJumping = false;
            boss.col.isTrigger = false;
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

    /*
     * checks if the enemy has jumped far enough
     * once enemy passes jumpLocation then jump will face the opposite dirction of startJumpDist
     */
    private bool IsJumpDone(Vector2 jump, BossState boss)
    {
        /*
         * end - start = boss.startJumpDist
         * end - curr = boss.jumpLocation - boss.rb.position
         * (end - start)*(end - curr) : >0 if same direction, or <0 if opposite directions
         */
        float val = Vector2.Dot((boss.startJumpDist).normalized, (boss.jumpLocation - boss.rb.position).normalized);

        if (val <= 0)
            return true;

        return false;
    }
}
