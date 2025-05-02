using UnityEngine;

/** Controls health logic. */
public class PlayerHealth : MonoBehaviour
{
    
    public int currentHealth;
    public int maxHealth;

    /** Changes the health of the object. */
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        // Object dies.
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false); // gameObject refers to object script is on.
        }
    }
}
