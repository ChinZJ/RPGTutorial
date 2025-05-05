using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    // Assigns health to enemy.
    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Changes health according to provided amount.
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        } else if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
