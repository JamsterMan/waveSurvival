using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpMovement : EnemyMovement
{
    enum JumpEnemyState {chase, jumpPrep, jumping };

    [SerializeField] private bool canJump = true;
    [SerializeField] private JumpEnemyState state = JumpEnemyState.chase;
    public float jumpCooldown = 5f;
    private float nextJumpTime = 0f;
    public float jumpSpeed = 20f;
    public float jumpDistance = 10f;
    public float jumpRange = 5f;

    private Collider2D col;

    private Vector2 startJumpDist = new Vector2();

    private void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.transform;
        playerH = playerObject.GetComponent<PlayerHealth>();

        attack = GetComponent<EnemyAttack>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        col = GetComponent<Collider2D>();
    }

    // Update is called at a fixed framerate
    private void FixedUpdate()
    {
        EnemyStateControl();

        if (!canJump)
        {
            if (Time.time > nextJumpTime)
            {
                canJump = true;
                //Debug.Log("can jump");
            }
        }
    }

    /*
     * decide what movement action to take based on state
     * decides when to switch states
     */
    private void EnemyStateControl()
    {
        Vector2 lookDirection = playerPos - rb.position;
        float angle;

        if (state == JumpEnemyState.jumpPrep)//enemy state
        {
            if (canJump)
                EnemyJump();

            if (lookDirection.magnitude > jumpRange && canJump)
            {
                state = JumpEnemyState.chase;
            }
        }
        else if (state == JumpEnemyState.jumping)
        {
            lookDirection = jumpLocation - rb.position;

            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;//face the jump loctaion

            if (IsJumpDone(lookDirection))
            {
                state = JumpEnemyState.chase;
                col.isTrigger = false;
            }

            if (lookDirection.magnitude != 0)
                movement = lookDirection / lookDirection.magnitude;

            rb.MovePosition(rb.position + (movement * jumpSpeed * Time.fixedDeltaTime));

            if (playerH.IsDamageable())
                attack.Attack();

        }
        else//state == JumpEnemyState.chase
        {
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;//face the player

            movement = lookDirection / lookDirection.magnitude;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            if (lookDirection.magnitude <= jumpRange && canJump)
            {
                state = JumpEnemyState.jumpPrep;
            }
        }
    }

    /*
     * Sets up variables for the jump attack
     * waits to add a delay before the jump happens
     */
    IEnumerator Jump()
    {
        //play pre-jump animation here

        col.isTrigger = true;//no collision while jumping  
        
        Vector2 lookDirection = (Vector2)player.position - rb.position;
        jumpLocation = rb.position + (lookDirection.normalized * jumpDistance);//point in space where the jump should end
        startJumpDist = jumpLocation - rb.position;
        canJump = false;
        nextJumpTime = Time.time + jumpCooldown;

        yield return new WaitForSeconds(0.5f);

        state = JumpEnemyState.jumping;
    }

    //starts Jump corutine
    public void EnemyJump()
    {
        StartCoroutine(Jump());
    }

    /*
     * checks if the enemy has jumped far enough
     * once enemy passes jumpLocation then jump will face the opposite dirction of startJumpDist
     */
    private bool IsJumpDone(Vector2 jump)
    {
        if ((startJumpDist + jump).magnitude > startJumpDist.magnitude)
            return false;
        else
            return true;
    }

    //stop jump state if wall is hit
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayBounds"))
        {
            state = JumpEnemyState.chase;
            col.isTrigger = false;
        }
    }
}
