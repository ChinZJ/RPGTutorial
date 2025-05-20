using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillSO skillSO;

    public Image skillIcon;
    public TMP_Text skillLevelText;
    public Button skillButton;

    public List<SkillSlot> prerequisiteSkillSlots;
    public int currentLevel;
    public bool isUnlocked;

    public static event Action<SkillSlot> OnAbilityPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;


    // Runs anytime changes are made to script variables.
    private void OnValidate()
    {
        if (skillSO != null && skillLevelText != null)
        {
            UpdateUI();
        }
    }

    // Upgrades skill upon call.
    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSO.maxLevel)
        {
            currentLevel++;
            OnAbilityPointSpent?.Invoke(this); // Null conditional operator.

            if (currentLevel >= skillSO.maxLevel)
            {
                OnSkillMaxed?.Invoke(this);
            }

            UpdateUI();
        }
    }

    public bool CanUnlockSkill()
    {
        foreach (SkillSlot slot in prerequisiteSkillSlots)
        {
            if (!slot.isUnlocked || slot.currentLevel < slot.skillSO.maxLevel)
            {
                return false;
            }
        }
        return true;
    }

    // Unlocks skill.
    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }

    // Updates skill text depending on level and lock status.
    private void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;

        if (isUnlocked)
        {
            skillButton.interactable = true;

            skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;

            skillLevelText.text = "Locked";
            skillIcon.color = Color.grey;
        }
    }
}
