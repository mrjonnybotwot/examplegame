using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

    public GameObject currentIntObject = null; //current interactable object
    public Interactable currentIntObjectScript = null; //refernce to script instance attached to object
    public Inventory inventory;
    public Interactable interactableStuff;

    private void Start()
    {
        interactableStuff = FindObjectOfType<Interactable>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentIntObject) //if push interact button and have interObject infocus
        {
            // checks to see if object can be added to inventory
            if (currentIntObjectScript.inventory) //go to currentIntObjects interactable script check inventory var for true or false
            {
                //add to inventory
                inventory.AddItemToInventory(currentIntObject); //sends current inter obj to add item of inventory script


            }//if currIntObject can be added to invent e.g. inventory box = true

        }

        //use an item
        if(Input.GetKeyDown(KeyCode.Q))
        {   //see if item in inventory
            GameObject healthItem = inventory.FindByItemType("Health Item"); //looks for gameobject of type health potion

            if(healthItem != null) //if item found
            {
                //healthItem.SendMessage("AddHealth");
                //use the item and apply its affect send message to DoInteraction of interactable script

                interactableStuff.AddHealth();
                inventory.RemoveItemFromInventory(healthItem); //make spot where item is null
            }
   
        }

        //just remove an item
        if (Input.GetKeyDown(KeyCode.R))
        {   //see if item in inventory
            GameObject healthItem = inventory.FindByItemType("Health Item"); //looks for gameobject of type health potion

            if (healthItem != null) //if item found
            {

                inventory.RemoveItemFromInventory(healthItem); //make spot where item is null
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision) //used for triggering when enter collider
    {
        if (collision.CompareTag("InteractableObject")) //if object i collide with has interact tag
        {
            Debug.Log(collision.name); //prints name of thing player collides with
            currentIntObject = collision.gameObject; //sets trigegr to current interactable object

            //grab currentintobjects Interactable script and assign to currentintobj script variable
            currentIntObjectScript = currentIntObject.GetComponent<Interactable>();
        }
    }

    void OnTriggerExit2D(Collider2D collision) //runs first frame out of collider
    {
        if (collision.CompareTag("InteractableObject")) //if has intObject tag
        {
            if(collision.gameObject == currentIntObject) //if just walked out of currentIntObject range
            {
                currentIntObject = null; //stops focusing on item and empties out item from  currentIntObject
            }
            
        }
    }
}
