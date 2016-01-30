using UnityEngine;
using System.Collections;

public class TestPlayer : MonoBehaviour {

    public float speed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;
    }
}
