using TMPro;
using UnityEngine;

/** Controls health logic. */
public class PlayerHealth : MonoBehaviour
{
    
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthText;
    public Animator healthTextAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    private void Start()
    {
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;
    }

    /** Changes the health of the object. */
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        healthTextAnim.Play("TextUpdate");
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;

        // Object dies.
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false); // gameObject refers to object script is on.
        }
    }
}
