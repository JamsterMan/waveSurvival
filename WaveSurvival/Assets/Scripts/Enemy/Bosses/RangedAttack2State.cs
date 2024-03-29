﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack2State : RangedAttackState
{
    private readonly int _maxShots = 6;
    private readonly int _minShots = 4;
    private readonly int _p2MaxShots = 8;
    private readonly int _p2MinShots = 4;


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
    }

    protected override void Shoot(BossState boss)
    {
        //play animations

        float angle = 0f;
        if (_shotCount % 2 == 1)
            angle = 45f;

        for (int i = 0; i < 4; i++)
        {
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

            Object.Instantiate(boss.rangedShot, boss.attackPoint.position, rot, boss.transform).transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            angle += 90f;
        }

        _shotCount++;
        _canShoot = false;
        _nextShootTime = Time.time + _timeBetweenShots;
    }

}
