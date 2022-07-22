using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedMovement : EnemyMovement
{
    enum RangedEnemyState { chase, shooting, reloading };

    [SerializeField] private bool canShoot = true;
    [SerializeField] private RangedEnemyState state = RangedEnemyState.chase;

    public float shootCooldown = 5f;
    public float backStepSpeed = 1.5f;
    private float nextShootTime = 0f;

    private void FixedUpdate()
    {
        Vector2 lookDirection = _playerPos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;
        _attack.attackPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (state == RangedEnemyState.shooting)//enemy state
        {
            if (canShoot)
            {
                _attack.Attack();//start shooting
                canShoot = false;
                nextShootTime = Time.time + shootCooldown;
                state = RangedEnemyState.reloading;
                
            }
            else
            {
                state = RangedEnemyState.reloading;
            }

            if (lookDirection.magnitude > _attack.attackRange)
            {
                state = RangedEnemyState.chase;
            }
        }
        else if (state == RangedEnemyState.reloading)
        {
            if(lookDirection.magnitude < _attack.attackRange / 2)
            {
                _movement = (lookDirection / lookDirection.magnitude)*-1;
                rb.MovePosition(rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
            }
            if (Time.time > nextShootTime)
            {
                canShoot = true;
                if (lookDirection.magnitude > _attack.attackRange)
                {
                    state = RangedEnemyState.chase;
                }
                else
                {
                    state = RangedEnemyState.shooting;
                }
            }
        }
        else // state == JumpEnemyState.chase
        {
            _movement = lookDirection / lookDirection.magnitude;
            rb.MovePosition(rb.position + _movement * backStepSpeed * Time.fixedDeltaTime);

            if (lookDirection.magnitude <= _attack.attackRange)
            {
                state = RangedEnemyState.shooting;
            }
        }

        if(lookDirection.magnitude <= 1.3)
        {
            ((EnemyRangedAttack)_attack).ContactAttack();
        }
    }
}
