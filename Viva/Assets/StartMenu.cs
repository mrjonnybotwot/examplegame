using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public void StartGame()
    {   //loads start level
        SceneManager.LoadScene("Main");
    }

    public void QuitTheGame()
    {
        Debug.Log("closeddddd");
        Application.Quit();
    }

    public void ReplayGame()
    {

        print("poooyyyyooooo");
        //playerHealth.playerCurrentHealth = playerHealth.playerMaxHealth;
        //SceneManager.LoadScene("Main");
        FindObjectOfType<HealthSystem>().ReplayGame();


    }
}
