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
        Vector2 lookDirection = playerPos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        if (state == RangedEnemyState.shooting)//enemy state
        {
            if (canShoot)
            {//playerH.IsDamageable())
                attack.Attack();//start shooting
                canShoot = false;
                nextShootTime = Time.time + shootCooldown;
                state = RangedEnemyState.reloading;
                
            }
            else
            {
                state = RangedEnemyState.reloading;
            }

            if (lookDirection.magnitude > attack.attackRange)
            {
                state = RangedEnemyState.chase;
            }
        }
        else if (state == RangedEnemyState.reloading)
        {
            if(lookDirection.magnitude < attack.attackRange / 2)
            {
                movement = (lookDirection / lookDirection.magnitude)*-1;
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            }
            if (Time.time > nextShootTime)
            {
                canShoot = true;
                Debug.Log("can Shoot");
                if (lookDirection.magnitude > attack.attackRange)
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
            movement = lookDirection / lookDirection.magnitude;
            rb.MovePosition(rb.position + movement * backStepSpeed * Time.fixedDeltaTime);

            if (lookDirection.magnitude <= attack.attackRange)
            {
                state = RangedEnemyState.shooting;
            }
        }
    }
}
