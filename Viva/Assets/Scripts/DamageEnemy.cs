using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour {

    public int damageToGive;    //damage done to enemy
    private int currentAtkDamage;
    public GameObject damageEffect;
    public Transform hitPoint;
    public GameObject damageValue;
    private Stat playerStats;

	// Use this for initialization
	void Start ()
    {
        playerStats = FindObjectOfType<Stat>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //current ATK damage is base damage of bullet + current attack value
            currentAtkDamage = damageToGive + playerStats.currentAttackValue;

            other.gameObject.GetComponent<EnemyHealthSystem>().DamageEnemy(currentAtkDamage);
            Instantiate(damageEffect, hitPoint.position, hitPoint.rotation);
            var clone = (GameObject) Instantiate(damageValue, hitPoint.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageValue = currentAtkDamage;
            clone.transform.position = new Vector2(hitPoint.position.x, hitPoint.position.y);
        }

    }
}
