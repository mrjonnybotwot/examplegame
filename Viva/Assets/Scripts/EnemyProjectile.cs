using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    public float moveSpeed;
    private Transform player;
    private Vector2 target;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
        //target is equal to player position when bullet spawns
	}
	
	// Update is called once per frame
	void Update ()
    {
        //proj moves towards player
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        //if projectile reaches its target in world destroy it
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
		
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.name == "Player" || collider.gameObject.tag == "Wall")
        {
            moveSpeed = 0;
            DestroyProjectile();
            
        }
    }

    

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
