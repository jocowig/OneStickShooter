using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float speed;
	// Use this for initialization
	    void Start ()
    {
    }

    void FixedUpdate ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 shootDirection;
            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - transform.position;
            transform.position += shootDirection * speed * Time.deltaTime;
        }
    }
}
