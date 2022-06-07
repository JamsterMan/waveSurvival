using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer enemySpriteRenderer;

    protected GameObject playerObject;
    protected Transform player;
    protected PlayerHealth playerH;
    protected EnemyAttack attack;

    protected Vector2 movement;
    protected Vector2 playerPos;

    private bool isAttacking = false;

    private void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.transform;
        playerH = playerObject.GetComponent<PlayerHealth>();

        attack = GetComponent<EnemyAttack>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.position;
    }

    // Update is called at a fixed framerate
    private void FixedUpdate()
    {
        Vector2 lookDirection = playerPos - rb.position;
        LookAtPlayer(lookDirection.x);

        if (isAttacking)//enemy state
        {
            if (playerH.IsDamageable())
                attack.Attack();

            if (lookDirection.magnitude > attack.attackRange)
            {
                isAttacking = false;
            }
        }
        else
        {
            movement = lookDirection / lookDirection.magnitude;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            if(lookDirection.magnitude <= attack.attackRange )//&& canJump)
            {
                isAttacking = true;
            }
        }
    }

    private void LookAtPlayer(float xdir)
    {
        if(xdir >= 0)//face to the right
        {
            enemySpriteRenderer.flipX = false;
        }
        else if(xdir < 0)//face to the left
        {
            enemySpriteRenderer.flipX = true;
        }
    }

}
