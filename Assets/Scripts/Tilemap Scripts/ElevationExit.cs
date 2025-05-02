using UnityEngine;

/** Determines exit behaviour of entry points to elevation objects. */
public class ElevationExit : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;

    /**
    * Disables Player collision with mountains upon passing last step.
    * Enables Player collision with boundaries of mountains.
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = true;
            }

            foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = false;
            }

            // Current highest tilemap renders at 10.
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
