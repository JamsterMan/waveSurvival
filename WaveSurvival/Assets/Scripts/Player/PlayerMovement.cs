using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MenuScript pauseMenu;
    public Rigidbody2D rb;
    public Camera cam;
    public PlayerHealth pHealth;
    public DodgeUI dodgeUI;
    public GameObject weapon;
    public GameObject firePoint;
    public Animator animator;
    public float moveSpeed = 5f;
    public float dodgeSpeed = 10f;
    public float dodgeCooldown = 3f;
    public float dodgeDuration = 1f;

    private float nextDodgetime = 0f;
    private float dodgeFinishTime = 0f;
    private bool isFlipped = false;
    private bool isMoving = false;

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
                if (!isMoving && movement.magnitude > 0.05)
                {
                    animator.SetBool("PlayerWalking", true);
                    isMoving = true;
                }
                else if(isMoving && movement.magnitude < 0.05)
                {
                    animator.SetBool("PlayerWalking", false);
                    isMoving = false;
                }
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
                else
                {
                    movement = movement.normalized;
                }
            }

            //cooldown for dodge
            if (!canDodge && Time.time > nextDodgetime)
            {
                canDodge = true;
                dodgeUI.UpdateDodgeUI(nextDodgetime , nextDodgetime, dodgeCooldown);//set fill to 0
                //activate border so cooldown finish more clear
            }
            else if (!canDodge)
                dodgeUI.UpdateDodgeUI(Time.time, nextDodgetime, dodgeCooldown);
        }
    }

    // Update is called at a fixed framerate
    private void FixedUpdate()
    {
        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if (isFlipped && angle >= -90f && angle <= 90f)
            FlipPlayer();
        else if (!isFlipped && (angle <= -90f || angle >= 90f))
            FlipPlayer();

        weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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

    /*
     * flips player to face right or left
     * flips weapon so it is always held pointing up
     * adjusts firepoint to make ranged attack always fire where the mouse is aiming
     */
    private void FlipPlayer()
    {
        if (isFlipped)
        {
            transform.localScale = new Vector3(1, 1, 1);
            weapon.transform.localScale = new Vector3(1, 1, 1);
            firePoint.transform.localScale = new Vector3(1, 1, 1);
            isFlipped = false;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            weapon.transform.localScale = new Vector3(-1, -1, 1);
            firePoint.transform.localScale = new Vector3(-1, -1, 1);
            isFlipped = true;
        }

    }

    public void ChangePlayerMoveSpeed(float amount)
    {
        if(moveSpeed + amount > 0f && moveSpeed + amount < 20f)//needs a max an min values
            moveSpeed += amount;
    }
}
