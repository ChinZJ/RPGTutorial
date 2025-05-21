using System.Collections;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;

    // Assigns Rigidbody2D to enemy.
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Applies knockback to enemy.
    public void Knockback(Transform forceTransform, float knockbackForce,
            float knockbackTime, float stunTime)
    {
        enemyMovement.ChangeState(EnemyState.Knockback);
        StartCoroutine(StunTimer(knockbackTime, stunTime));

        Vector2 direction = (transform.position - forceTransform.position).normalized;
        rb.linearVelocity = direction * knockbackForce;

        Debug.Log("Knockback applied.");
    }

    IEnumerator StunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemyMovement.ChangeState(EnemyState.Idle);
    }
}
