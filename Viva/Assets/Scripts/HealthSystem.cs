using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{

    public int playerMaxHealth;
    public int playerCurrentHealth;
    public GameObject uiStuff;
    public GameObject respawnPoint;
    private CameraController theCamera;

    private Stat playerStats;
    bool gameIsOver;
    private Inventory inventory;

    // Use this for initialization
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        playerStats = FindObjectOfType<Stat>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHealth <= 0)
        {
            PlayerDead();
        }
        

    }

    public void PlayerDead()
    {
        
            Debug.Log("am i working");
            playerCurrentHealth = 0;
            //gameObject.SetActive(false);
            playerStats.currentLevel = 0;
            Debug.Log("p22222222ooooo");
            EndGame();
            Debug.Log("p2ooooo");
        
    }

    public void EndGame()
    {
        Debug.Log("hehehe");
        Debug.Log("game is over  = " + gameIsOver);

        if (gameIsOver == false)
        {
            Debug.Log("am i being called lol");

            //uiStuff.SetActive(false);
            SceneManager.LoadScene("Deado");
            gameIsOver = true;
        }

    }

    public void ReplayGame()
    {
        gameIsOver = false;
        Debug.Log("game is over after replay button is set to = " + gameIsOver);
        print("gamegayyyyManagerReplay");
        //playerHealth.playerCurrentHealth = playerHealth.playerMaxHealth;
        SceneManager.LoadScene("Main");
        FindObjectOfType<HealthSystem>().RestartGame();


    }

    public void RestartGame()
    {
        
            theCamera = FindObjectOfType<CameraController>();
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
        
            playerStats.currentPlayerXP = 0;
            playerStats.currentLevel = 0;
            playerStats.currentHPValue = playerStats.HPLevel[playerStats.currentLevel];
            playerCurrentHealth = playerMaxHealth;

            

        transform.position = new Vector3(respawnPoint.transform.position.x, respawnPoint.transform.position.y, respawnPoint.transform.position.z);
        inventory = FindObjectOfType<Inventory>();
        GameObject healthItem = inventory.FindByItemType("Health Item"); //looks for gameobject of type health potion
        inventory.RemoveItemFromInventory(healthItem);

        //gameObject.SetActive(true);




    }

    public void DamagePlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;

        StartCoroutine("HurtColor");
    }

    IEnumerator HurtColor() //coroutine for transparency when damaged
    {
        for (int i = 0; i < 3; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f); //Red, Green, Blue, Alpha/Transparency - goes trans
            yield return new WaitForSeconds(.3f);   //invis
            GetComponent<SpriteRenderer>().color = Color.white; //back to original
            yield return new WaitForSeconds(.1f);   //waits for .1 of sec
   
        }
    } //This IEnumerator runs 3 times, resulting in 3 flashes.﻿

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    
}
