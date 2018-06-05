using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewLevel : MonoBehaviour {

    public string levelToLoad;
    public string ExitPoint;

    private Player thePlayer;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(levelToLoad);
            thePlayer.startPoint = ExitPoint;
        }
        
    }
}
