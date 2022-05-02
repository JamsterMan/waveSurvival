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

            movement = lookDirection / lookDirection.magnitude;

            rb.MovePosition(rb.position + (movement * jumpSpeed * Time.fixedDeltaTime));

            if (playerH.IsDamageable())
                attack.Attack();

            if (rb.position.magnitude <= jumpLocation.magnitude + 0.3f && rb.position.magnitude >= jumpLocation.magnitude - 0.3f)
            {
                isJumping = false;
                facePlayer = true;
            }
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
        Vector2 lookDirection = (Vector2)player.position - rb.position;
        jumpLocation = rb.position + (lookDirection.normalized * jumpDistance);//point in space where the jump should end
        //jumpLocation = player.position;
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
     * add function to tell if the jump is finished
     * based on the lookdirection of the jump
     */
}
