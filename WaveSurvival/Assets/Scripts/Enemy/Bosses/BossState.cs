using System.Collections;
using UnityEngine;

public class BossState : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float p2MoveSpeed = 7f;
    public float attackRange = 1f;
    public int attackDamage = 1;
    public int p2AttackDamage = 2;
    public float minRangedAttackRange = 2f;
    public bool phase2 = false;
    public bool isWaiting = false;

    /// Temp Jump Vars*************************************************************************************
    public float jumpCooldown = 5f;
    public float jumpSpeed = 20f;
    public float jumpDistance = 10f;
    public float jumpRange = 5f;
    public Vector2 movement;
    //[SerializeField] public bool isJumping = false;

    public Vector2 jumpLocation = new Vector2();
    public Collider2D col;
    public Vector2 startJumpDist = new Vector2();
    public JumpAttackState jumpAttackState = new JumpAttackState();
    /// Temp jump Vars End***********************************************************************

    public Transform attackPoint;
    public Vector2 attackPointSize;
    public LayerMask PlayerLayer;

    public Vector2 playerPos;

    public Animator animator;
    public Rigidbody2D rb;

    protected GameObject _playerObject;
    public Transform player;
    public PlayerHealth playerH;
    public SpriteRenderer bossSpriteRenderer;

    public GameObject rangedShot;

    private State _currState;
    public ChaseState chaseState = new ChaseState();
    public AttackState attackState = new AttackState();
    public RangedAttackState rangedAttackState = new RangedAttackState();

    private void Start()
    {
        _currState = chaseState;

        _currState.EnterState(this);

        _playerObject = GameObject.Find("Player");
        player = _playerObject.transform;
        playerH = _playerObject.GetComponent<PlayerHealth>();

        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.position;
        _currState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        _currState.FixedUpdateState(this);
    }

    /*
     * swtichs current state to given state
     */
    public void SwitchState(State state)
    {
        animator.SetBool("EnemyMove", false);
        _currState = state;
        _currState.EnterState(this);
    }

    /*
     * starts phase 2 of a boss (if it exists)
     */
    public void SetPhase2()
    {
        phase2 = true;
    }

    /*
     * makes boss always face the player
     */
    public void LookAtPlayer(float xdir)
    {
        if (xdir >= 0)//face to the right
        {
            bossSpriteRenderer.flipX = false;
        }
        else if (xdir < 0)//face to the left
        {
            bossSpriteRenderer.flipX = true;
        }
    }


    //draws circle in editor to show attack range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireCube(attackPoint.position, attackPointSize);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        Gizmos.DrawWireSphere(this.transform.position, minRangedAttackRange);
    }






    /*****************************************************************
     * wait functionality bellow
     ******************************************************************/

    /*
     * a wait function to be used on some attacks
     */
    IEnumerator Wait()
    {
        //play Wait animation here

        isWaiting = true;

        yield return new WaitForSeconds(0.5f);

        isWaiting = false;
    }

    //starts Jump corutine
    public void EnemyWait()
    {
        StartCoroutine(Wait());
    }

    //stop any state if wall is hit
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayBounds"))
        {
            isWaiting = false;
            col.isTrigger = false;
            SwitchState(chaseState);
        }
    }

}
