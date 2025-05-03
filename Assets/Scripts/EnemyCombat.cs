using UnityEngine;

/** Controls enemy combat logic. */
public class EnemyCombat : MonoBehaviour
{
    // Change to static in future? Currently following tutorial.
    public int damage = 1;

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
}
