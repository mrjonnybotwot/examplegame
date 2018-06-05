using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform player;
	Vector3 target, mousePos, refVel, shakeOffset;
	float cameraDist = 3.5f;
	float smoothTime = 0.2f, zStart;
	//shake
	float shakeMag, shakeTimeEnd;
	Vector3 shakeVector;
	bool shaking;

    private static bool cameraExists;
    public BoxCollider2D mapBoundaryBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    void Start () {
		target = player.position; //set default target
		zStart = transform.position.z; //capture current z position
        DontDestroyOnLoad(transform.gameObject);

        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        minBounds = mapBoundaryBox.bounds.min; //returns lowest bound e.g. bot left corner
        maxBounds = mapBoundaryBox.bounds.max; //returns highest bounds e.g. top right

        theCamera = GetComponent<Camera>(); //refernce to cam
        halfHeight = theCamera.orthographicSize; //orth cam size is half of full cam size
        halfWidth = halfHeight * Screen.width / Screen.height;
    }
	void Update () {
		mousePos = CaptureMousePos(); //find out where the mouse is
		shakeOffset = UpdateShake(); //account for screen shake
		target = UpdateTargetPos(); //find out where the camera ought to be
		UpdateCameraPosition(); //smoothly move the camera closer to it's target location

        if(mapBoundaryBox == null)
        {
            mapBoundaryBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
            minBounds = mapBoundaryBox.bounds.min;
            maxBounds = mapBoundaryBox.bounds.max;
        }

        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        //so cam doesnt go too far too left or right

        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        //so cam doesnt go too far up or down

        transform.position = new Vector3(clampedX, clampedY, transform.position.z); 
        //applys clamped values to cam position
    }
    Vector3 CaptureMousePos(){
		Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition); //raw mouse pos
		ret *= 2; 
		ret -= Vector2.one; //set (0,0) of mouse to middle of screen
		float max = 0.9f;
		if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max){
			ret = ret.normalized; //helps smooth near edges of screen
		}
		return ret;
	}
	Vector3 UpdateTargetPos(){
		Vector3 mouseOffset = mousePos * cameraDist; //mult mouse vector by distance scalar 
		Vector3 ret = player.position + mouseOffset; //find position as it relates to the player
		ret += shakeOffset; //add the screen shake vector to the target
		ret.z = zStart; //make sure camera stays at same Z coord
		return ret;
	}
	Vector3 UpdateShake(){
		if (!shaking || Time.time > shakeTimeEnd){
			shaking = false; //set shaking false when the shake time is up
			return Vector3.zero; //return zero so that it won't effect the target
		}
		Vector3 tempOffset = shakeVector; 
		tempOffset *= shakeMag; //find out how far to shake, in what direction
		return tempOffset;
	}
	void UpdateCameraPosition(){
		Vector3 tempPos;
		tempPos = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime); //smoothly move towards the target
		transform.position = tempPos; //update the position
	}

	public void Shake(Vector3 direction, float magnitude, float length){ //capture values set for where it's called
		shaking = true; //to know whether it's shaking
		shakeVector = direction; //direction to shake towards
		shakeMag = magnitude; //how far in that direction
		shakeTimeEnd = Time.time + length; //how long to shake
	}

    public void SetBounds(BoxCollider2D newBounds) //tells cam to use these bounds 
    {
        mapBoundaryBox = newBounds;
        minBounds = mapBoundaryBox.bounds.min; //returns lowest bound e.g. bot left corner
        maxBounds = mapBoundaryBox.bounds.max; //returns highest bounds e.g. top right
    }
}