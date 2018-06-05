using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour {

    public float moveSpeed;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;

    private Rigidbody2D myRigidBody;

    public bool currentlyWalking;

    public float walkTime;
    public float waitTime;
    private float walkCounter;
    private float waitCounter;

    private int walkDirection;  //what direction to walk in

    public BoxCollider2D walkArea;
    private bool hasWalkArea;   //checked for if NPC has walkArea
    public bool canMove;

    private DialogueManager theDM;
    
    // Use this for initialization
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        theDM = FindObjectOfType<DialogueManager>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();//so NPC starts moving from start game

        if(walkArea != null)    //if walkarea assigned
        { 
            minWalkPoint = walkArea.bounds.min; //gets the min point of boxcollider boundary (bottom left corner)
            maxWalkPoint = walkArea.bounds.max; //gets the max point of boxcollider boundary (top right corner)
            hasWalkArea = true;
        }

        canMove = true;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!theDM.dialogueActive) //if dialogues not active
        {
            canMove = true;
        }

        if (!canMove)
        {
            myRigidBody.velocity = Vector2.zero;
            return; //stops rest of update
        }

		if(currentlyWalking)
        {
            walkCounter -= Time.deltaTime; //counts down time currently walking


            switch (walkDirection)//checks walk direction
            {
                case 0:
                    myRigidBody.velocity = new Vector2(0, moveSpeed); //Moves up only
                    if (hasWalkArea && transform.position.y > maxWalkPoint.y)//if NPC reaches top of collider
                    {
                        currentlyWalking = false;   //stops walking    
                        waitCounter = waitTime; //switches waitime to waiting for set time
                    }
                    break;

                case 1:
                    myRigidBody.velocity = new Vector2(moveSpeed, 0); //moves right only
                    if (hasWalkArea && transform.position.x > maxWalkPoint.x)//if NPC reaches far right of collider
                    {
                        currentlyWalking = false;   //stops walking    
                        waitCounter = waitTime; //switches waitime to waiting for set time
                    }
                    break;

                case 2:
                    myRigidBody.velocity = new Vector2(0, -moveSpeed); //moves down only
                    if (hasWalkArea && transform.position.y < minWalkPoint.y)//if NPC reaches bottom of collider
                    {
                        currentlyWalking = false;   //stops walking    
                        waitCounter = waitTime; //switches waitime to waiting for set time
                    }
                    break;

                case 3:
                    myRigidBody.velocity = new Vector2(-moveSpeed, 0); //moves left only
                    if (hasWalkArea && transform.position.x < maxWalkPoint.x)//if NPC reaches far left of collider
                    {
                        currentlyWalking = false;   //stops walking    
                        waitCounter = waitTime; //switches waitime to waiting for set time
                    }
                    break;
            }

            if (walkCounter < 0)
            {
                currentlyWalking = false;   //stopes walking    
                waitCounter = waitTime; //switches waitime to waiting for set time
            }
        }
        else
        {
            waitCounter -= Time.deltaTime; //counts down time currently waiting

            myRigidBody.velocity = Vector2.zero; //movespeed is 0 when not moving

            if (waitCounter < 0) //when waittime over
            {
                ChooseDirection();
            }
        }
	}

    public void ChooseDirection()   
    {
        walkDirection = Random.Range(0, 4); //numbers between 0,123
        currentlyWalking = true;    //start moving
        walkCounter = walkTime; //resets walkCounter value

    }//used for  choosing direction represented as 0,1,2,3
}
