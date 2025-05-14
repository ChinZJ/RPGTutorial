using TMPro;
using UnityEngine;

/** Controls health logic. */
public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Animator healthTextAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    private void Start()
    {
        healthText.text = "HP: " + StatsManager.Instance.currentHealth
                + " / " + StatsManager.Instance.maxHealth;
    }

    /** Changes the health of the object. */
    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;

        healthTextAnim.Play("TextUpdate");
        healthText.text = "HP: " + StatsManager.Instance.currentHealth
                + " / " + StatsManager.Instance.maxHealth;

        // Object dies.
        if (StatsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false); // gameObject refers to object script is on.
        }
    }
}
