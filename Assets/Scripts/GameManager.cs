using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    
    private void Awake() {
        // PlayerPrefs.DeleteAll();
        if (GameManager.instance != null) {
            Destroy(gameObject);
            return;
        }

        // PlayerPrefs.DeleteAll();

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Ressources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public PlayerController player;
    public WeaponController weapon;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int pesos;
    public int experience;

    // Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Upgrade Weapon
    public bool TryUpdateWeapon() {

        // Is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel) {
            return false;
        }

        if (pesos >= weaponPrices[weapon.weaponLevel]) {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // Experience System
    public int GetCurrentLevel() {
        int r = 0;
        int add = 0;

        while (experience >= add) {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) { // Max Level
                return r;
            }
        }

        return r;
    }

    public int GetXpToLevel(int level) {

        int r = 0;
        int xp = 0;

        while (r < level) {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXp(int xp) {

        int currentLevel = GetCurrentLevel();
        experience += xp;
        if (currentLevel < GetCurrentLevel()) {
            OnLevelUp();
        }
    }

    public void OnLevelUp() {
        Debug.Log("Level Up!");
        player.OnLevelUp();
    }

    // Save state
    /*
     * INT preferedSkin
     * INT pesos
     * INT experience
     * INT weaponLevel
     */
    public void SaveState() {
        Debug.Log("SaveState");
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode) {

        Debug.Log("LoadState");

        // SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState")) {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        // 0|30|15|2

        // Change player skin
        pesos = int.Parse(data[1]);

        // Experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1) {
            player.SetLevel(GetCurrentLevel());
        }

        // Change the weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }
}
