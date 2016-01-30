using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {

    public float speed = 10f;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = new Vector3(speed, speed);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 norm = GetComponent<Rigidbody>().velocity.normalized;

        norm.z = 0;

        GetComponent<Rigidbody>().velocity = norm * speed;
    }
}
