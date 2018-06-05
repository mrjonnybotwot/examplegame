using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Slider healthBar;
    public Text HPText;
    public HealthSystem playerHealth;
    private static bool UIExists;
    private Stat playerStats;
    public Text levelText;

	// Use this for initialization
	void Start ()
    {
        if (!UIExists)
        {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerStats = GetComponent<Stat>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        healthBar.maxValue = playerHealth.playerMaxHealth;
        healthBar.value = playerHealth.playerCurrentHealth;
        HPText.text = playerHealth.playerCurrentHealth + "/" + playerHealth.playerMaxHealth;
        levelText.text = "Lvl: " + playerStats.currentLevel;
	}
}
