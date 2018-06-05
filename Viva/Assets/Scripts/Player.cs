using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :MonoBehaviour
{

    bool mouseLeft, canShoot;
    float lastShot = 0, timeBetweenShots = 0.5f;
    Vector3 mousePos, mouseVector;
    public Transform gunSprite, gunTip;
    public SpriteRenderer gunRend;
    int playerSortingOrder = 20;
    public GameObject bulletPrefab;
    CameraController Cam;

    private static bool playerExists;
    public string startPoint;

    private Vector2 moveInput;

    public float moveSpeed;
    private float currentMoveSpeed;
    private Animator animator; //reference to animator
    private bool playerMoving;
    private Vector2 lastMove;

    public bool canMove;

    private Rigidbody2D myRigidbody;

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        GetMouseInput();
        Cam = FindObjectOfType<CameraController>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }else
        {
            Destroy(gameObject);
        }

        canMove = true;
    }

    void GetMouseInput()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //position of cursor in world
        mousePos.z = transform.position.z; 
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
     void Update ()
    {
        playerMoving = false;

        if (!canMove)
        {
            myRigidbody.velocity = Vector2.zero;
            return;
        }

        GetInput();
        Animation(); //rotate the gun
        Shooting(); //handle shooting

    }

    
    private void GetInput()
    {
        //gets input on horizontal and vertical axis is normalised so diagonal speed the same
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if(moveInput != Vector2.zero) //if getting input
        {
            //applys forces to rigidbody
            myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
            playerMoving = true;
            lastMove = moveInput;
        }
        else{ //for when no input
            myRigidbody.velocity = Vector2.zero;
        }

        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        animator.SetBool("PlayerMoving", playerMoving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);

        GetMouseInput();
    }

    
}
