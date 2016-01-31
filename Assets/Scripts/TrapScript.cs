using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {

	float originalSpeed;
	float timeLimit = .5f;
	float elapsed = 0f;

	bool canStop = true;

    public string trapType;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		if (!canStop) {
			elapsed += Time.deltaTime;
			if (elapsed > timeLimit) {
				canStop = true;
			}
		}
	}
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trap Collision Entered");
        if (other.tag.Equals("Player"))
        {
            if (trapType == "damage")
            {
                //Debug.Log("Entered Damage Collider");
                other.gameObject.GetComponent<PlayerScript>().reduceHealthBar();
            }
            if (trapType == "slow" && canStop)
            {
				originalSpeed = PlayerScript.maxSpeed;
				PlayerScript.speed = 1;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            if (trapType == "slow")
            {
				elapsed = 0;
				canStop = false;
                PlayerScript.speed = originalSpeed;
            }
        }
    }
}
