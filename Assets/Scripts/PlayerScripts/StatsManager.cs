using UnityEngine;

// Stores all Player stats.
public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    
    [Header("Combat Stats")] // Header within Unity inspector bar.
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header("Movement Stats")]
    public int speed;

    [Header("Health Stats")]
    public int maxHealth;
    public int currentHealth;

    // Ensures only a single isntance of StatsManager exists.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
}
