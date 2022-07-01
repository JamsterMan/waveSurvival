using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
    private enum State {chase, attack, charge, p2Chase, p2Attack, p2Charge}
    [SerializeField]private State bossState = State.chase;

    public float moveSpeed = 5f;
    public float attackRange = 1f;
    public Rigidbody2D rb;

    protected GameObject playerObject;
    protected Transform player;
    protected PlayerHealth playerH;

    protected Vector2 movement;
    protected Vector2 playerPos;

    private void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.transform;
        playerH = playerObject.GetComponent<PlayerHealth>();

        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.position;
    }

    private void FixedUpdate()
    {
        Vector2 lookDirection = playerPos - rb.position;

        if (bossState == State.chase)//chase player
        {
            movement = lookDirection / lookDirection.magnitude;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            if (lookDirection.magnitude <= attackRange)//&& canJump)
            {
                bossState = State.attack;
            }
        }
        else if (bossState == State.attack)//attack the player
        {
            
            if (lookDirection.magnitude > attackRange)
            {
                bossState = State.chase;
            }
        }
        else if (bossState == State.charge)//charge at the player
        {

        }
        /*else if (bossState == State.p2Chase)//faster chase
        {

        }
        else if (bossState == State.p2Attack)//stronger attack
        {

        }
        else if (bossState == State.p2Charge)//faster charge
        {

        }*/
    }



    //draws circle in editor to show attack range
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
