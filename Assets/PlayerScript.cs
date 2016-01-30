using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public Rigidbody rb;
    public float speed;
	// Use this for initialization
	    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed / Time.deltaTime);

        Vector3 shootDirection;
          shootDirection = Input.mousePosition;
          shootDirection.z = 0.0f;
          shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
          shootDirection = shootDirection - transform.position;

        rb.AddForce(-shootDirection * speed / Time.deltaTime);
    }
}
