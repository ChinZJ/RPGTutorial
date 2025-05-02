using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls movement logic.
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5; // How fast player moves.
    public int facingDirection = 1; // Default facing right.
    public Rigidbody2D rb; // Handles physics.
    public Animator anim; // For animations.

    // Update is called 50 times per frame.
    void FixedUpdate()
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
        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;
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
}
