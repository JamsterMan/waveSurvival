using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpMovement : EnemyMovement
{
    enum JumpEnemyState {chase, jumpPrep, jumping };

    [SerializeField] private bool canJump = true;
    [SerializeField] private JumpEnemyState _state = JumpEnemyState.chase;
    public float jumpCooldown = 5f;
    private float _nextJumpTime = 0f;
    public float jumpSpeed = 20f;
    public float jumpDistance = 10f;
    public float jumpRange = 5f;

    private Vector2 _jumpLocation = new Vector2();
    private Collider2D _col;

    private Vector2 _startJumpDist = new Vector2();

    private void Start()
    {
        _playerObject = GameObject.Find("Player");
        _player = _playerObject.transform;
        _playerH = _playerObject.GetComponent<PlayerHealth>();

        _attack = GetComponent<EnemyAttack>();
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        _col = GetComponent<Collider2D>();
    }

    // Update is called at a fixed framerate
    private void FixedUpdate()
    {
        EnemyStateControl();

        if (!canJump)
        {
            if (Time.time > _nextJumpTime)
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
        Vector2 lookDirection = _playerPos - rb.position;
        //float angle;

        if (_state == JumpEnemyState.jumpPrep)//enemy state
        {
            if (canJump)
                EnemyJump();

            if (lookDirection.magnitude > jumpRange && canJump)
            {
                _state = JumpEnemyState.chase;
            }
        }
        else if (_state == JumpEnemyState.jumping)
        {
            lookDirection = _jumpLocation - rb.position;

            if (IsJumpDone(lookDirection))
            {
                _state = JumpEnemyState.chase;
                _col.isTrigger = false;
            }

            if (lookDirection.magnitude != 0)
                _movement = lookDirection / lookDirection.magnitude;

            rb.MovePosition(rb.position + (_movement * jumpSpeed * Time.fixedDeltaTime));

            if (_playerH.IsDamageable())
                _attack.Attack();

        }
        else//state == JumpEnemyState.chase
        {

            _movement = lookDirection / lookDirection.magnitude;
            rb.MovePosition(rb.position + _movement * moveSpeed * Time.fixedDeltaTime);

            if (lookDirection.magnitude <= jumpRange && canJump)
            {
                _state = JumpEnemyState.jumpPrep;
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

        _col.isTrigger = true;//no collision while jumping  
        
        Vector2 lookDirection = (Vector2)_player.position - rb.position;
        _jumpLocation = rb.position + (lookDirection.normalized * jumpDistance);//point in space where the jump should end
        _startJumpDist = _jumpLocation - rb.position;
        canJump = false;
        _nextJumpTime = Time.time + jumpCooldown;

        yield return new WaitForSeconds(0.5f);

        _state = JumpEnemyState.jumping;
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
        /*
         * end - start = boss.startJumpDist
         * end - curr = boss.jumpLocation - boss.rb.position
         * (end - start)*(end - curr) : >0 if same direction, or <0 if opposite directions
         */
        float val = Vector2.Dot((_startJumpDist).normalized, (_jumpLocation - rb.position).normalized);

        if (val <= 0)
            return true;

        return false;
    }

    //stop jump state if wall is hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayBounds"))
        {
            _state = JumpEnemyState.chase;
            _col.isTrigger = false;
        }
    }
}
