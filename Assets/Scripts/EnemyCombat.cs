using UnityEngine;

/** Controls enemy combat logic. */
public class EnemyCombat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask playerLayer; // Check if player is in range.

    /**
    * Triggers upon collision with Player object.
    *
    * @param collision The object of collision.
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
        }
    }

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
        }

    }
}
