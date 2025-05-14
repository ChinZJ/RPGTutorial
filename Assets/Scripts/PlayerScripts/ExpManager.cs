using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Governs Player experience points.
public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float expGrowthMultiplier = 1.2f; // Add 20% more EXP to level each time.
    
    public Slider expSlider;
    public TMP_Text currentLevelText;

    // Handles main logic of Player experience gain.
    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    // Updates Player level and determines next threshold.
    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);
    }

    // Updates Player experience leve UI.
    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level " + level;
    }

    // Adds GainExperience method to the enemy object (listener).
    private void OnEnable()
    {
        EnemyHealth.OnMonsterDefeated += GainExperience;
    }

    // Removes GainExperience method to the enemy object (listener).
    private void OnDisable()
    {
        EnemyHealth.OnMonsterDefeated -= GainExperience;
    }

    // Test code for gaining experience points.
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GainExperience(2);
        }
    }
}
