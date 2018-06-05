using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour {

    private BoxCollider2D mapBoundaryBox; //refernce to box collider
    private CameraController theCamera; //refercne to cam

	// Use this for initialization
	void Start ()
    {
        mapBoundaryBox = GetComponent<BoxCollider2D>(); //finds map boundarys
        theCamera = FindObjectOfType<CameraController>(); //find cam
        theCamera.SetBounds(mapBoundaryBox); //bounds = to that levels bounds
		
	}
	
}
