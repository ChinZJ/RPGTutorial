using TMPro;
using UnityEngine;

// Governs modification of Player UI stats.
public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;

    private bool statsOpen = false;

    // Updates Player stats upon creation of Player object.
    private void Start()
    {
        UpdateAllStats();
    }

    // Updates Player stats.
    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
        {
            if (statsOpen)
            {
                Time.timeScale = 1;
                UpdateAllStats();
                statsCanvas.alpha = 0;
                statsOpen = false;
            } else 
            {
                Time.timeScale = 0;
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsOpen = true;
            }
            
        }
    }

    // Updates all Player stats.
    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
    }

    // Updates the damage stat of the Player.
    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>(). text = "Damage: "
                + StatsManager.Instance.damage;
    }

    // Updates the speed stat of the Player.
    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>(). text = "Speed: "
                + StatsManager.Instance.speed;
    }
}
