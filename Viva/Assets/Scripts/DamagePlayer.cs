using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public int damageToGive;
    private int currentAtkDamage;
    public GameObject damageValue;
    private Stat playerStats;

	// Use this for initialization
	void Start () {
        playerStats = FindObjectOfType<Stat>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
         if (collider.gameObject.name == "Player")
        {
            //current damage to player is base damage - players current defense stats
            currentAtkDamage = damageToGive - playerStats.currentDefenseValue;

            if(currentAtkDamage < 0)
            {
                currentAtkDamage = 0;
            }

            collider.gameObject.GetComponent<HealthSystem>().DamagePlayer(currentAtkDamage);

            var clone = Instantiate(damageValue, collider.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageValue = currentAtkDamage;
            clone.transform.position = new Vector2(collider.transform.position.x, collider.transform.position.y);

        }
    }
}
