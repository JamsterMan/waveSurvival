using UnityEngine;

public class RangedAttackState : State
{
    protected int _shotCount = 0;
    protected int _shots = 0;
    protected bool _canShoot = true;
    protected float _nextShootTime = 0f;
    protected float _timeBetweenShots = 0.3f;
    private readonly int _maxShots = 5;
    private readonly int _minShots = 2;
    private readonly int _p2MaxShots = 7;
    private readonly int _p2MinShots = 3;


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

    public override void UpdateState(BossState boss)
    {
        //playerPos = boss.player.position;

        if (!_canShoot && Time.time > _nextShootTime)
        {
            _canShoot = true;
        }
        CheckStateSwitch(boss);
    }

    public override void FixedUpdateState(BossState boss)
    {
        Vector2 lookDirection = boss.playerPos - boss.rb.position;
        boss.LookAtPlayer(lookDirection.x);
        if (_canShoot)
            Shoot(boss);

    }

    protected virtual void Shoot(BossState boss)
    {
        //play animations

        Vector2 lookDirection = boss.playerPos - boss.rb.position;
        boss.LookAtPlayer(lookDirection.x);
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

        Object.Instantiate(boss.rangedShot, boss.attackPoint.position, rot, boss.transform).transform.localScale = new Vector3(0.5f,0.5f,0.5f);

        _shotCount++;
        _canShoot = false;
        _nextShootTime = Time.time + _timeBetweenShots;
    }

    public override void CheckStateSwitch(BossState boss)
    {

        if (_shotCount == _shots)
        {
            boss.SwitchState(boss.chaseState);
        }
    }
}
