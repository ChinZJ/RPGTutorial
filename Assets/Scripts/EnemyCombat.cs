using System.Collections;
using UnityEngine;

/** Controls enemy combat logic. */
public class EnemyCombat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float knockbackForce;
    public float stunTime;
    public float weaponRange;
    public LayerMask playerLayer; // Check if player is in range.

    // Deals damage to Player object if detected.
    public void Attack()
    {
        Debug.Log("Attacking Player");

        // Obtain all objects within weapon range surrounding the attack point on the player layer.
        Collider2D[] hits = Physics2D.OverlapCircleAll(
                attackPoint.position, weaponRange, playerLayer);

        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            hits[0].GetComponent<PlayerMovement>().Knockback(transform, knockbackForce, stunTime);
        }

    }
}
