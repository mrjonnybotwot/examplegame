using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{

    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    private Stat playerStats;
    public int expToAdd;

    // Use this for initialization
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;

        playerStats = FindObjectOfType<Stat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);

            playerStats.GainExperience(expToAdd);
        }
    }

    public void DamageEnemy(int damageToGive)
    {
        enemyCurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }
}
