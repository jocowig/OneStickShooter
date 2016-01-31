﻿using UnityEngine;
using System.Collections;
using System;

public class FollowEnemy : MonoBehaviour {

    private GameObject powerUp;
    public GameObject healthPowerUp;
    public GameObject speedPowerUp;
    public GameObject weaponPowerUp;
    public GameObject player;
    public float followTime = 1f;
    public float followSpeed = 2f;
    private float elapsedTime = 0f;
    private float currentHealth;
    public float maxHealth;
    public GameObject healthBar;

	public bool followPlayer = false;

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
	    if (player != null && followPlayer)
        {
            elapsedTime += Time.deltaTime;
            //Debug.Log("elapesed time " + elapsedTime);
            if (elapsedTime > followTime)
            {
                iTween.MoveTo(this.gameObject, player.transform.position, followSpeed);
                elapsedTime = 0f;
               // Debug.Log("Following");
            }
        }
	}

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag.Equals("Firework"))
        {
			followPlayer = true;
            currentHealth = currentHealth - collisionInfo.gameObject.GetComponent<FireworkScript>().damage;
            reduceHealthBar(currentHealth / maxHealth);
            if(currentHealth<=0)
            {
                possiblyDropPowerUp();
                Destroy(this.gameObject);    
            }
        }
    }

    private void possiblyDropPowerUp()
    {
        float rand = UnityEngine.Random.value;
        if(rand< .05)
        {
            powerUp = Instantiate(weaponPowerUp) as GameObject;
            powerUp.transform.position = transform.position;
        }
        else if(rand < .13)
        {
            powerUp = Instantiate(speedPowerUp) as GameObject;
            powerUp.transform.position = transform.position;
        }
        else if(rand< .25)
        {
            powerUp = Instantiate(healthPowerUp) as GameObject;
            powerUp.transform.position = transform.position;
        }
    }

    void reduceHealthBar(float remainingHealth)
    {
        healthBar.transform.localScale = new Vector3(remainingHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
