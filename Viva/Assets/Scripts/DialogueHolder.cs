using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour {


    public string dialogue; //dialogue villager says
    private DialogueManager dManager;

    public string[] dialogueLines; //holds lines of dialogue
	// Use this for initialization
	void Start ()
    {
        dManager = FindObjectOfType<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision) //every moment player is inside zone
    {
        if(collision.gameObject.name == "Player") //if player collides with zone
        {
            if (Input.GetKeyUp(KeyCode.E)) //if player presses space
            {
                //dManager.ShowDialogueBox(dialogue); //loads dialogue box with text

                if (!dManager.dialogueActive)
                {
                    dManager.dialogueLines = dialogueLines; //equal to whats currently stored in lines
                    dManager.currentDialogueLine = 0;
                    dManager.ShowDialogue(); //will activaate dialoguebox
                }

                if (transform.parent.GetComponent<NPCMovement>() != null)
                {
                    transform.parent.GetComponent<NPCMovement>().canMove = false;

                }//gets transform of parent e.g villager/if can find villager movement
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //when player enters inside zone
    {
        if (gameObject.name == "doorLocked" && collision.gameObject.name == "Player") //if locked doors
        {
            
                if (!dManager.dialogueActive)
                {
                    dManager.dialogueLines = dialogueLines; //equal to whats currently stored in lines
                    dManager.currentDialogueLine = 0;
                    dManager.ShowDialogue(); //will activate dialoguebox
                }
            
        }
    }
}
