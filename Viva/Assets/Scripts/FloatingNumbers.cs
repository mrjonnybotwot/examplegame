using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour {

    public float floatSpeed;
    public int damageValue;
    public Text displayValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        displayValue.text = "" + damageValue;  //converts int damage to string
        transform.position = new Vector3(transform.position.x, transform.position.y + (floatSpeed * Time.deltaTime), transform.position.z); //floats value up y axis
	}
}
