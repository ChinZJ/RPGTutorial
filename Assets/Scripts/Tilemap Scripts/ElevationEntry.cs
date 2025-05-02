using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mono.Cecil.Cil;
using UnityEngine;

/** Determines entry behaviour of entry points to elevation objects. */
public class ElevationEntry : MonoBehaviour
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
                mountain.enabled = false;
            }

            foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = true;
            }

            // Current highest tilemap renders at 10.
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
}
