using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls movement logic.
public class PlayerMovement : MonoBehaviour
{
    private bool isKnockedback;
    private int facingDirection = 1; // Default facing right.
    public Rigidbody2D rb; // Handles physics.
    public Animator anim; // For animations.
    public PlayerCombat playerCombat;

    // Update Player upon attack.
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            playerCombat.Attack();
        }
    }

    // Update is called 50 times per frame.
    void FixedUpdate()
    {
        if (isKnockedback == false)
        {
            // Obtain inputs.
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Ensures player is facing the correct direction.
            if ((horizontal > 0 && transform.localScale.x < 0) || 
                    (horizontal < 0 && transform.localScale.x > 0))
            {
                Flip();
            }

            // Sets animator's floats to mirror absolute input value.
            // Conditions within the animation is set > 0.1.
            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

            // Note: original code uses velocity but it is obsolete.
            rb.linearVelocity = new Vector2(horizontal, vertical) * StatsManager.Instance.speed;
        }
        
    }

    /**
    * Flips the direction the character is facing
    */
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
    }

    // Creates knockback for Player.
    public void Knockback(Transform enemy, float knockbackForce, float stunTime)
    {
        // Disable movment
        isKnockedback = true;

        // Apply velocity
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.linearVelocity = direction * knockbackForce;

        StartCoroutine(KnockbackCounter(stunTime)); // Can pause and resume without blocking game.
    }

    /**
    * Stuns Player movement for duration of knockback.
    * Note to self: IEnumerator is a collection of objects that can be enumerated.
    */
    IEnumerator KnockbackCounter(float stunTime)
    {
        // Temporarily pauses execution and returns control until condition (WaitForSeconds) is met.
        yield return new WaitForSeconds(stunTime); 
        rb.linearVelocity = Vector2.zero;
        isKnockedback = false;

    }
}
