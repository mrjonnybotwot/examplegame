using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public GameObject dialogueBox;
    public Text dialogueText;

    public bool dialogueActive;

    public string[] dialogueLines;
    public int currentDialogueLine;

    private Player thePlayer;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(dialogueActive && Input.GetKeyUp(KeyCode.E))
        {
            currentDialogueLine++; //move onto next line
        }

        if (currentDialogueLine >= dialogueLines.Length)
        {
            dialogueBox.SetActive(false); //when length reached deactivate box
            dialogueActive = false;

            currentDialogueLine = 0; //resets dialogue to start
            thePlayer.canMove = true;

        }//check if current line exceeds num of lines in array

        dialogueText.text = dialogueLines[currentDialogueLine];
    }

    public void ShowDialogueBox(string dialogue) //send text to this
    {
        dialogueActive = true;
        dialogueBox.SetActive(true); //activates dialogue box
        dialogueText.text = dialogue; //makes text shown whatever dialogue i pass through
    }

    public void ShowDialogue()
    {
        dialogueActive = true;
        dialogueBox.SetActive(true); //activates dialogue box
        thePlayer.canMove = false;
    }
}
