using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenuController : MonoBehaviour {

    // Text fields
    public Text levelText, hitPointText, pesosText, upgradeCostText, xpText;

    // Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick(bool right) {

        if (right) {
            currentCharacterSelection++;

            // If we want too far away
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count) {
                currentCharacterSelection = 0;
            }

            OnSelectionChanged();
        }
        else {
            currentCharacterSelection--;

            // If we want too far away
            if (currentCharacterSelection < 0) {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged() {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade
    public void OnUpgradeClik() {

        if (GameManager.instance.TryUpdateWeapon()) {
            UpdateMenu();
        }
    }

    // Update the character Information
    public void UpdateMenu() {

        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count) {
            upgradeCostText.text = "MAX";
        }
        else {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }

        // Meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitPointText.text = GameManager.instance.player.hitPoints.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();

        // xp Bar
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if (currentLevel == GameManager.instance.xpTable.Count) {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points"; // Display total xp
            xpBar.localScale = Vector3.one;
        }
        else {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currentLevelXP = GameManager.instance.GetXpToLevel(currentLevel);

            int diff = currentLevelXP - prevLevelXp;
            int currentXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currentXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currentXpIntoLevel.ToString() + " / " + diff;
        }
        // xpText.text = "NOT IMPLEMENTED";
        // xpBar.localScale = new Vector3(0.5f, 0, 0);
    }

}
