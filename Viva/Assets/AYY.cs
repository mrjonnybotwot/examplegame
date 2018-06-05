using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AYY : MonoBehaviour {

    //Static is shared between every instance of this class
    public static AYY _AYY_SINGLETON;

	// Use this for initialization
	void Start () {
        //Singleton design pattern - ensures only one of this object
        if (_AYY_SINGLETON == null)
        {
            _AYY_SINGLETON = this;
        }
        else
        //in this case, variable has already been assigned
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
