using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {
    public string trapType;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trap Collision Entered");
        if (other.tag.Equals("Player"))
        {
            if (trapType == "damage")
            {
                Debug.Log("Entered Damage Collider");
                other.gameObject.GetComponent<PlayerScript>().reduceHealthBar();
            }
            if (trapType == "slow")
            {
                other.gameObject.GetComponent<PlayerScript>().speed = 1;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            if (trapType == "slow")
            {
                other.gameObject.GetComponent<PlayerScript>().speed = 5;
            }
        }
    }
}
