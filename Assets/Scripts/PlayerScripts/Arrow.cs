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
        }
    }
}
