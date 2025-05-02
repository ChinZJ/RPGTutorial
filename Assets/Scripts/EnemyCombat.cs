using UnityEngine;

/** Controls enemy combat logic. */
public class EnemyCombat : MonoBehaviour
{
    // Change to static in future? Currently following tutorial.
    public int damage = 1;

    /**
    * Triggers upon collision with object.
    *
    * @param collision The object of collision.
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
    }
}
