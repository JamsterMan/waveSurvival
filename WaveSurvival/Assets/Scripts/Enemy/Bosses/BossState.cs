﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float p2MoveSpeed = 7f;
    public float attackRange = 1f;
    public int attackDamage = 1;
    public int p2AttackDamage = 2;
    public float minRangedAttackRange = 2f;
    public bool phase2 = false;

    public Transform attackPoint;
    public Vector2 attackPointSize;
    public LayerMask PlayerLayer;

    //public Boss bossHealth;
    public Rigidbody2D rb;

    protected GameObject playerObject;
    public Transform player;
    public PlayerHealth playerH;

    public GameObject rangedShot;

    private State currState;
    public ChaseState chaseState = new ChaseState();
    public AttackState attackState = new AttackState();
    public RangedAttackState rangedAttackState = new RangedAttackState();

    private void Start()
    {
        currState = chaseState;

        currState.EnterState(this);

        playerObject = GameObject.Find("Player");
        player = playerObject.transform;
        playerH = playerObject.GetComponent<PlayerHealth>();

        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        currState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currState.FixedUpdateState(this);
    }

    public void SwitchState(State state)
    {
        currState = state;
        currState.EnterState(this);
    }

    public void SetPhase2()
    {
        phase2 = true;
    }


    //draws circle in editor to show attack range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireCube(attackPoint.position, attackPointSize);
        Gizmos.DrawWireSphere(this.transform.position, minRangedAttackRange);
    }
}
