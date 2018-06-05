using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    bool gameIsOver;
    public GameObject uiStuff;
    public HealthSystem playerHealth;
    public bool playerCanDie;

    public GameObject player;

    public void Start()
    {

        //playerHealth = FindObjectOfType<HealthSystem>();

        

    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }//if
        else if (instance != this) {
            Destroy(gameObject);
        }//else if

        DontDestroyOnLoad(gameObject); //isnt destroyed in all scenes

    }//Awake

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

}
