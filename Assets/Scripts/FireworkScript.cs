using UnityEngine;
using System.Collections;

public class FireworkScript : MonoBehaviour {

    GameObject Explosion;
    public float speed = 10f;
    public Vector3 dir;
    public float bulletLife = 2f;
    private float elapsedTime = 0f;
    GameObject instExplosion;

    // Use this for initialization
    void Start () {
        Explosion = Resources.Load("FireWorkExplosion") as GameObject;
	}

    // Update is called once per frame
    void Update()
    {

        transform.GetComponent<Rigidbody>().velocity = dir * speed;

        elapsedTime += Time.deltaTime;
        if (elapsedTime > bulletLife)
        {
            instExplosion = Instantiate(Explosion) as GameObject;
            instExplosion.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            Destroy(this.gameObject);

        }

        //Debug.Log("Elapsed " + elapsedTime + " Bullet life " + bulletLife);
    }
    void OnTriggerEnter(Collider collisionInfo)
    {
        instExplosion = Instantiate(Explosion) as GameObject;
        instExplosion.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        Destroy(this.gameObject);
    }
}
