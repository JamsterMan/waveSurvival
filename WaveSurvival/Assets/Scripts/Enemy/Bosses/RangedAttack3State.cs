using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack3State : RangedAttackState
{
    private readonly int _maxShots = 6;
    private readonly int _minShots = 6;
    private readonly int _p2MaxShots = 12;
    private readonly int _p2MinShots = 12;

    float angle = 0f;


    public override void EnterState(BossState boss)
    {
        //Debug.Log("Entered Ranged Attack State");
        _shotCount = 0;
        if (boss.phase2)
        {
            _shots = Random.Range(_p2MinShots, _p2MaxShots + 1);//+1 since max is exclusive
            _timeBetweenShots = 0.2f;
        }
        else
        {
            _shots = Random.Range(_minShots, _maxShots + 1);//+1 since max is exclusive
            _timeBetweenShots = 0.3f;
        }
        angle = 0f;
    }

    protected override void Shoot(BossState boss)
    {
        //play animations

        for (int i = 0; i < 4; i++)
        {
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

            Object.Instantiate(boss.rangedShot, boss.attackPoint.position, rot, boss.transform).transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            angle += 15f;
        }

        _shotCount++;
        _canShoot = false;
        _nextShootTime = Time.time + _timeBetweenShots;
    }
}
