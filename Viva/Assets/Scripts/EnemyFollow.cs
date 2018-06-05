using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    public float moveSpeed;
    private Transform player;
    public float stoppingDistance;
    public int retreatDistance;

    private float timeBetweenShots;
    public float startTimeBetweenShots;
    public GameObject projectile;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        timeBetweenShots = startTimeBetweenShots;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //distance between enemy and player dtermines move toward
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
        }

        //if 0 fires projectile
        if (timeBetweenShots <= 0)
        {
            //spawns proj with no rotation
            Instantiate(projectile, transform.position, Quaternion.identity);
            //if dont do this enemy will shoot every fame
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}
