using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : State
{
    private Vector2 playerPos;
    private int shotCount = 0;
    private int shots = 0;
    private readonly int maxShots = 5;
    private readonly int minShots = 2;
    private readonly int p2MaxShots = 7;
    private readonly int p2MinShots = 3;
    private bool canShoot = true;
    private float nextShootTime = 0f;
    private float timeBetweenShots = 0.3f;


    public override void EnterState(BossState boss)
    {
        Debug.Log("Entered Ranged Attack State");
        shotCount = 0;
        if (boss.phase2)
        {
            shots = Random.Range(p2MinShots, p2MaxShots + 1);//+1 since max is exclusive
            timeBetweenShots = 0.2f;
        }
        else
        {
            shots = Random.Range(minShots, maxShots + 1);//+1 since max is exclusive
            timeBetweenShots = 0.3f;
        }
    }

    public override void UpdateState(BossState boss)
    {
        playerPos = boss.player.position;

        if (!canShoot && Time.time > nextShootTime)
        {
            canShoot = true;
        }
    }

    public override void FixedUpdateState(BossState boss)
    {
        if(canShoot)
            Shoot(boss);

        if (shotCount == shots)
        {
            boss.SwitchState(boss.chaseState);
        }

    }

    private void Shoot(BossState boss)
    {
        //play animations

        Vector2 lookDirection = playerPos - boss.rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

        Object.Instantiate(boss.rangedShot, boss.attackPoint.position, rot, boss.transform).transform.localScale = new Vector3(0.5f,0.5f,0.5f);

        shotCount++;
        canShoot = false;
        nextShootTime = Time.time + timeBetweenShots;
    }
}
