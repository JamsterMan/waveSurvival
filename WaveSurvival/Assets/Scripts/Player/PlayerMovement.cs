using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MenuScript pauseMenu;
    public Rigidbody2D rb;
    public Camera cam;
    public PlayerHealth pHealth;
    public float moveSpeed = 5f;
    public float dodgeSpeed = 10f;
    public float dodgeCooldown = 3f;
    public float dodgeDuration = 1f;

    private float nextDodgetime = 0f;
    private float dodgeFinishTime = 0f;

    [SerializeField]private bool isDodging = false;
    [SerializeField] private bool canDodge = true;

    Vector2 movement;
    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.IsGamePaused())
        {
            if (!isDodging)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

            }

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetKeyDown(KeyCode.Space) && canDodge)//dodge
            {
                dodgeFinishTime = Time.time + dodgeDuration;
                nextDodgetime = Time.time + dodgeCooldown;
                isDodging = true;
                canDodge = false;
                pHealth.DodgeIframes(dodgeFinishTime);

                if (movement.magnitude < 0.05)
                {
                    Vector2 dodgeDirection = rb.position - mousePos;
                    movement = dodgeDirection.normalized;

                }
            }

            //cooldown for dodge
            if (!canDodge && Time.time > nextDodgetime)
            {
                canDodge = true;
                Debug.Log("player can dodge");
            }
        }
    }

    // Update is called at a fixed framerate
    private void FixedUpdate()
    {
        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;

        if (isDodging)
        {
            PlayerDodge();
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void PlayerDodge()
    {
        rb.MovePosition(rb.position + movement * dodgeSpeed * Time.fixedDeltaTime);

        if (Time.time > dodgeFinishTime)
        {
            isDodging = false;
        }
    }

    public void ChangePlayerMoveSpeed(float amount)
    {
        if(moveSpeed + amount > 0f && moveSpeed + amount < 20f)//needs a max an min values
            moveSpeed += amount;
    }
}
