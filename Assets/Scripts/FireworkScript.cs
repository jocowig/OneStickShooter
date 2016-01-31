using UnityEngine;
using System.Collections;
using System;

public class FireworkScript : MonoBehaviour {

    GameObject Explosion;
    public float speed = 10f;
    public Vector3 dir;
    public float bulletLife = 2f;
    private float elapsedTime = 0f;
    GameObject instExplosion;
    public int damage;

    // Use this for initialization
    void Start () {
        Explosion = Resources.Load("FireWorkExplosion") as GameObject;
		if (UnityEngine.Random.value > .4) {
			AudioSource audio = gameObject.AddComponent<AudioSource> ();
			audio.pitch = UnityEngine.Random.Range (0.65f, 1.15f);
			audio.clip = AudioManager.Instance.GetRandomFirework ();
			audio.loop = false;
			audio.spatialBlend = 1.0f;
			audio.volume = UnityEngine.Random.Range (0.45f, .75f);
			audio.Play ();
		}
	}

    // Update is called once per frame
    void Update()
    {

        transform.GetComponent<Rigidbody>().velocity = dir * speed;

        elapsedTime += Time.deltaTime;
        if (elapsedTime > bulletLife)
        {
            try {
                instExplosion = Instantiate(Explosion) as GameObject;
                instExplosion.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                Destroy(this.gameObject);
            }
            catch(ArgumentException e)
            {

            }
        }

        //Debug.Log("Elapsed " + elapsedTime + " Bullet life " + bulletLife);
    }
    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag.Equals("Enemy") || collisionInfo.tag.Equals("Wall"))
        {
            try
            {
                instExplosion = Instantiate(Explosion) as GameObject;
                instExplosion.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                Destroy(this.gameObject);
            }
            catch (ArgumentException e)
            {

            }
        }
    }
}
