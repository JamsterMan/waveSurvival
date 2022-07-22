using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer enemySpriteRenderer;

    protected GameObject _playerObject;
    protected Transform _player;
    protected PlayerHealth _playerH;
    protected EnemyAttack _attack;

    protected Vector2 _movement;
    protected Vector2 _playerPos;

    private bool _isAttacking = false;

    private void Start()
    {
        _playerObject = GameObject.Find("Player");
        _player = _playerObject.transform;
        _playerH = _playerObject.GetComponent<PlayerHealth>();

        _attack = GetComponent<EnemyAttack>();
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        _playerPos = _player.position;
    }

    // Update is called at a fixed framerate
    private void FixedUpdate()
    {
        Vector2 lookDirection = _playerPos - rb.position;
        LookAtPlayer(lookDirection.x);

        if (_isAttacking)//enemy state
        {
            if (_playerH.IsDamageable())
                _attack.Attack();

            if (lookDirection.magnitude > _attack.attackRange)
            {
                _isAttacking = false;
            }
        }
        else
        {
            _movement = lookDirection / lookDirection.magnitude;
            rb.MovePosition(rb.position + _movement * moveSpeed * Time.fixedDeltaTime);

            if(lookDirection.magnitude <= _attack.attackRange )//&& canJump)
            {
                _isAttacking = true;
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
