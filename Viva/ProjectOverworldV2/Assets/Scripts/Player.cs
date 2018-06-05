﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :Character
{
    
    bool mouseLeft, canShoot;
    float lastShot = 0, timeBetweenShots = 0.25f;
    Vector3 mousePos, mouseVector;
    public Transform gunSprite, gunTip;
    public SpriteRenderer gunRend;
    int playerSortingOrder = 20;
    public GameObject bulletPrefab;
    CameraController Cam;

    protected override void Start()
    {
        GetMouseInput();
        Cam = FindObjectOfType<CameraController>();
        base.Start();
    }

    void GetMouseInput()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //position of cursor in world
        mousePos.z = transform.position.z; //keep the z position consistant, since we're in 2d
        mouseVector = (mousePos - transform.position).normalized; //normalized vector from player pointing to cursor
        mouseLeft = Input.GetMouseButton(0); //check left mouse button
    }

    void Animation()
    {
        float gunAngle = -1 * Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg; //find angle in degrees from player to cursor
        gunSprite.rotation = Quaternion.AngleAxis(gunAngle, Vector3.back); //rotate gun sprite around that angle
        gunRend.sortingOrder = playerSortingOrder - 1; //put the gun sprite bellow the player sprite
        if (gunAngle > 0)
        { //put the gun on top of player if it's at the correct angle
            gunRend.sortingOrder = playerSortingOrder + 1;
        }
    }

    void Shooting()
    {
        canShoot = (lastShot + timeBetweenShots < Time.time);
        if (mouseLeft && canShoot)
        { //shoot if the mouse button is held and its been enough time since last shot
            Vector3 spawnPos = gunTip.position; //position of the tip of the gun, a transform that is a child of rotating gun
            Quaternion spawnRot = Quaternion.identity; //no rotation, bullets here are round
            Bullet bul = Instantiate(bulletPrefab, spawnPos, spawnRot).GetComponent<Bullet>();//spawn bullet and capture it's script
            bul.Setup(mouseVector); //give the bullet a direction to fly
            lastShot = Time.time; //used to check next time this is called
            Cam.Shake((transform.position - gunTip.position).normalized, 1.5f, 0.05f); //call camera shake for recoil
        }
    }

    // Update is called once per frame
    protected override void Update ()
    {
        GetInput();
        Animation(); //rotate the gun
        Shooting(); //handle shooting
        base.Update(); //executes characters update
	}

    
    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }

        GetMouseInput();
    }
}
