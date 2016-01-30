using UnityEngine;
using System.Collections;

public class FireworkScript : MonoBehaviour {

    public float speed = 10f;
    public Vector3 dir;
    public float bulletLife = 2f;
    private float elapsedTime = 0f;

	// Use this for initialization
	void Start () {
       
	}

    // Update is called once per frame
    void Update()
    {

        transform.GetComponent<Rigidbody>().velocity = dir * speed;

        elapsedTime += Time.deltaTime;
        if (elapsedTime > bulletLife)
        {
            Destroy(this.gameObject);
        }

        //Debug.Log("Elapsed " + elapsedTime + " Bullet life " + bulletLife);
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        Destroy(this.gameObject);
    }
}
