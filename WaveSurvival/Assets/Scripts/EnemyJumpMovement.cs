using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpMovement : EnemyMovement
{
    [SerializeField] private bool facePlayer = true;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool canJump = true;
    public float jumpCooldown = 5f;
    private float nextJumpTime = 0f;
    public float jumpSpeed = 20f;
    public float jumpDistance = 10f;

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
        Vector2 lookDirection = playerPos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        if (facePlayer)
            rb.rotation = angle;//face the player

        if (isAttacking)//enemy state
        {
            if (canJump)
                ((EnemyJumpAttack)attack).Jump();

            if (lookDirection.magnitude > ((EnemyJumpAttack)attack).jumpRange && canJump)
            {
                isAttacking = false;
            }
        }
        else if (isJumping)
        {
            lookDirection = jumpLocation - rb.position;

            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;//face the jump loctaion

            if (IsJumpDone(lookDirection))
            {
                isJumping = false;
                facePlayer = true;
                col.isTrigger = false;
            }

            if (lookDirection.magnitude != 0)
                movement = lookDirection / lookDirection.magnitude;

            rb.MovePosition(rb.position + (movement * jumpSpeed * Time.fixedDeltaTime));

            if (playerH.IsDamageable())
                attack.Attack();

        }
        else
        {
            movement = lookDirection / lookDirection.magnitude;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            if (lookDirection.magnitude <= ((EnemyJumpAttack)attack).jumpRange && canJump)
            {
                isAttacking = true;
            }
        }

        if (!canJump)
        {
            if (Time.time > nextJumpTime)
            {
                canJump = true;
                Debug.Log("can jump");
            }
        }
    }

    IEnumerator Jump()
    {
        col.isTrigger = true;   
        Vector2 lookDirection = (Vector2)player.position - rb.position;
        jumpLocation = rb.position + (lookDirection.normalized * jumpDistance);//point in space where the jump should end
        startJumpDist = jumpLocation - rb.position;
        canJump = false;
        facePlayer = false;
        nextJumpTime = Time.time + jumpCooldown;

        yield return new WaitForSeconds(0.5f);

        isJumping = true;
        isAttacking = false;
    }

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
            isJumping = false;
            facePlayer = true;
            col.isTrigger = false;
        }
    }
}
