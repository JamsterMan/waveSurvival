using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    private GameObject playerObject;
    private Transform player;
    private PlayerHealth playerH;
    private EnemyAttack attack;

    Vector2 movement;
    Vector2 playerPos;

    public bool isAttacking = false;

    private void Start()
    {
        playerObject = GameObject.Find("Player");
        Debug.Log(playerObject.name);
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
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        if (isAttacking)
        {
            if (playerH.isDamageable())
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

            if(lookDirection.magnitude <= attack.attackRange)
            {
                isAttacking = true;
            }
        }
    }
}
