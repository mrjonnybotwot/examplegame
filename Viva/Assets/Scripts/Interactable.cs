using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool inventory;  //if true (ticked in inspector) this object can be stored in inventory
    private Stat playerStats;
    public int healthToAdd;
    public string itemType; //tells what type of item this object is

    private PlayerInteract playerInteractStuff;

    private void Start()
    {
        playerStats = FindObjectOfType<Stat>();
        DontDestroyOnLoad(gameObject); //so item stays in inventory in every scene


    }

    public void DoInteraction()
    {
        
        //playerStats.AddItemExperience(healthToAdd); //method in Stat script used to apply health to current health in stat

        //picked up and put in inventory
        gameObject.SetActive(false); //makes gameobject disapear
    }

    public void AddHealth()
    {
        playerStats.AddItemExperience(healthToAdd); //method in Stat script used to apply health to current health in stat

    }
}
