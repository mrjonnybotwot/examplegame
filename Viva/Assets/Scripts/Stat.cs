using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private HealthSystem playerHealth;

    public int currentLevel;

    public int currentPlayerXP;

    public int [] toNextLevelUp;

    public int[] HPLevel;
    public int[] attackLevel;
    public int[] defenseLevel;

    public int currentHPValue;
    public int currentAttackValue;
    public int currentDefenseValue;

    public int poo = 0;

    [SerializeField] //field appears in inspector
    private int baseValue;

    public int GetValue()
    {
        return baseValue;
    }

    public void Start()
    {
        playerHealth = FindObjectOfType<HealthSystem>();

        //starts player at level 1
        currentHPValue = HPLevel[1];
        currentAttackValue = attackLevel[1];
        currentDefenseValue = defenseLevel[1];
    }

    public void Update()
    {
        if(currentPlayerXP >= toNextLevelUp[currentLevel])
        {
            LevelUp();  //if enough XP calls levelUp function
        }
    }

    public void GainExperience(int experienceToAdd)
    {
        currentPlayerXP += experienceToAdd;
    }

    //used for health pick ups
    public void AddItemExperience(int healthToAdd)
    {
        //currentPlayerXP += extraXP;

        //player health from healthsystem = players current health + item health value
        playerHealth.playerCurrentHealth = playerHealth.playerCurrentHealth + healthToAdd;

        if (playerHealth.playerCurrentHealth > playerHealth.playerMaxHealth)
        {
            playerHealth.playerCurrentHealth = playerHealth.playerMaxHealth;
        }

    }

    public void LevelUp()
    {
        currentLevel++; //increments to next level
        currentHPValue = HPLevel[currentLevel]; //HP based on current Level

        playerHealth.playerMaxHealth = currentHPValue;

        //finds difference inbetween HP levels and adds it to player health
        playerHealth.playerCurrentHealth += currentHPValue - HPLevel[currentLevel - 1];

        //atk and dfs values are upgraded to current lvl values
        currentAttackValue = attackLevel[currentLevel];
        currentDefenseValue = defenseLevel[currentLevel];
    }
}

