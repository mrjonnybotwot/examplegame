using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject[] inventory = new GameObject [3];
    public Button [] InventoryButtons = new Button[3]; //STORES ALL BUTTONS IN A BUTTON ARRAY
    
    public void AddItemToInventory(GameObject item) //link to item to be added (currentIntObject)
    {
        bool itemAdded = false; //no item added yet
        
        //find the first open slot in inventory
        for(int i =0; i < inventory.Length; i++)//loops through inventory array
        {
            if(inventory[i] == null) //if finds empty spot in array
            {
                inventory[i] = item; //adds item to empty spot position

                //update UI
                InventoryButtons[i].image.overrideSprite = item.GetComponent<SpriteRenderer>().sprite;
                //in array ovverrides that buttons sprite with item sprite from renderer of sprite


                Debug.Log(item.name + "was added");
                itemAdded = true; //an ietm added to inventory

                //do something with the object from Interactable script
                item.SendMessage("DoInteraction");
                break; //stop looping through loop
            }
        }

        //if full inventory
        if (!itemAdded) //if false
        {
            Debug.Log("Inventory full - Item not added");
        }

    }//for adding item to inventory

    public GameObject FindByItemType(string itemType)
    {
        for(int i =0; i < inventory.Length; i++)//loop through array
        {
            if(inventory[i] != null) //if something in array spot
            {
                if(inventory [i].GetComponent <Interactable>().itemType == itemType) 
                {   //gets item type from Interactable script and checks if it matches item type were looking for

                    //returns gameObject to what called this function
                    return inventory[i];
                }
            }
        }
        return null; //if not item with correct item type found
    }

    public void RemoveItemFromInventory(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)//loop through array
        {
          
            if (inventory[i] == item) //if item to remove found in array
            {   //gets item type from Interactable script and checks if it matches item type were looking for
                //item.SendMessage("AddHealth");
                inventory[i] = null; //empties current slot in inventory
                InventoryButtons[i].image.overrideSprite = null; //overides button sprite back to original for when item removed
                Debug.Log(item.name + " was removed from inventory");
                break;
            }
            
        }
    }
}
