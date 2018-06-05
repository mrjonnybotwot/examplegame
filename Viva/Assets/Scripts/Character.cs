using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Animator animator; //reference to animator

    protected Vector2 direction; //used protected as could accidentally change values if public by mistake

    private Rigidbody2D myRigidbody;

    private Vector2 lastMove;

    public bool playerMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    // Use this for initialization
    protected virtual void Start () {

        myRigidbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>(); //gets the animator object from player
	}
	
	// Update is called once per frame
	protected virtual void Update () //overides update method in player
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        myRigidbody.velocity = direction.normalized * speed;
        
    }

    public void HandleLayers()
    {
        if (playerMoving)
        {
            AnimateMovement(direction);
        }
    }

    public void AnimateMovement(Vector2 direction)
    {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }
}

