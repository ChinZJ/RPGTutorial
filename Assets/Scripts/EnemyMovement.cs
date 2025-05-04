using UnityEditor.Tilemaps;
using UnityEngine;

/** Controls enemy movement. */
public class EnemyMovement : MonoBehaviour
{
    // References to self.
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyState enemyState;

    public float speed;

    private int facingDirection = 1;
    public Transform detectionPoint; // Centre point of enemy circle of sight.

    private float attackRange = 1.2F;
    public float attackCooldown = 2;
    private float attackCooldownTimer;   

    public float playerDetectRange = 5; 

    // References to player.
    private Transform player;
    public LayerMask playerLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame.
    void Update()
    {
        CheckForPlayer();

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime; // Measures time passed since last frame.
        }

        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        } else if (enemyState == EnemyState.AttackingSide)
        {
            rb.linearVelocity = Vector2.zero; // Stop movement when attacking
        }
        
    }

    // Updates direction.
    void Chase()
    {
        if ((player.position.x > transform.position.x && facingDirection == -1) ||
            (player.position.x < transform.position.x && facingDirection == 1))
        {
            Flip();
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    // Flips the direction of the enemy.
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
    }

    /** 
    * Changes enemy state depending on detection of Player.
    */
    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
                detectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0)
        {
            player = hits[0].transform;

            // If Player is in attack range AND cooldown is ready.
            if ((Vector2.Distance(transform.position, player.position) <= attackRange)
                    && (attackCooldownTimer <= 0))
            {
                attackCooldownTimer = attackCooldown;

                // Calculate angle between enemy and player.
                Vector2 direction = (player.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                // Normalize angle to be between 0 and 360.
                if (angle < 0)
                {
                    angle += 360;
                }
                    
                // Determine animation to use.
                if (angle > 45 && angle < 135)
                {
                    ChangeState(EnemyState.AttackingUp);
                }
                else if (angle > 225 && angle < 315)
                {
                    ChangeState(EnemyState.AttackingDown);
                }
                else
                {
                    ChangeState(EnemyState.AttackingSide);
                }

            } else if ((Vector2.Distance(transform.position, player.position) > attackRange) &&
                    !((enemyState == EnemyState.AttackingDown) ||
                    (enemyState == EnemyState.AttackingUp) ||
                    (enemyState == EnemyState.AttackingSide)))
            {
                // The 
                ChangeState(EnemyState.Chasing);
            }
        } else 
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    // Changes state of enemy.
    void ChangeState(EnemyState newState)
    {
        // Exit current animation.
        switch (enemyState)
        {
            case EnemyState.Idle:
                anim.SetBool("isIdle", false);
                break;
            case EnemyState.Chasing:
                anim.SetBool("isChasing", false);
                break;
            case EnemyState.AttackingSide:
                anim.SetBool("isAttackingSide", false);
                break;
            case EnemyState.AttackingUp:
                anim.SetBool("isAttackingUp", false);
                break;
            case EnemyState.AttackingDown:
                anim.SetBool("isAttackingDown", false);
                break;
            // Could throw exception as a default.
        }

        // Update current state.
        enemyState = newState;

        // Update new animation.
        switch (enemyState)
        {
            case EnemyState.Idle:
                anim.SetBool("isIdle", true);
                break;
            case EnemyState.Chasing:
                anim.SetBool("isChasing", true);
                break;
            case EnemyState.AttackingSide:
                anim.SetBool("isAttackingSide", true);
                break;
            case EnemyState.AttackingUp:
                anim.SetBool("isAttackingUp", true);
                break;
            case EnemyState.AttackingDown:
                anim.SetBool("isAttackingDown", true);
                break;    
            // Could throw exception as a default.
        }
    }

    // Draws detection range of enemy object.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    AttackingSide,
    AttackingUp,
    AttackingDown,

}
