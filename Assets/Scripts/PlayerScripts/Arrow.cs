using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;

    public float lifeSpan = 2;
    public float speed;

    public int damage;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    public LayerMask enemyLayer;
    public LayerMask obstacleLayer; // Layers that the arrow sticks into.

    public SpriteRenderer sr; // Allows change of arrow appearance.
    public Sprite buriedSprite; // Upon lodging of arrow object.

    void Start()
    {
        rb.linearVelocity = direction * speed;
        RotateArrow();

        Destroy(gameObject, lifeSpan);
    }

    private void RotateArrow()
    {
        float angle = (float)Math.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Layers are single bits, thus the bitwise shift operator.
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<EnemyHealth>().ChangeHealth(-damage);
            collision.gameObject.GetComponent<EnemyKnockback>()
                    .Knockback(transform, knockbackForce, knockbackTime, stunTime);
            AttachToTarget(collision.gameObject.transform);
        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            AttachToTarget(collision.gameObject.transform);
        }
    }

    private void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSprite;

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic; // No longer reacts to kinematics.

        transform.SetParent(target); // Arrow follows target.
    }
}
