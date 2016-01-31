using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {

    public static float speed = 7.5f;
    public GameObject player;
    private float currentHealth;
    public static float maxHealth = 25;
    public GameObject healthBar;

    public bool followPlayer = false;

	public GameObject portal;

	AudioSource sound;

    // Use this for initialization
    void Start()
    {
    	currentHealth = maxHealth;
    	GetComponent<Rigidbody>().velocity = new Vector3(speed, speed);
		sound = gameObject.AddComponent<AudioSource> ();
		sound.clip = AudioManager.Instance.boss;
		sound.loop = false;
		//sound.spatialBlend = 1f;
	}

    // Update is called once per frame
    void Update()
    {
		if (followPlayer) {
			Vector3 norm = GetComponent<Rigidbody> ().velocity.normalized;

			norm.z = 0;

			GetComponent<Rigidbody> ().velocity = norm * speed;
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		} else {
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		}
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag.Equals("Firework"))
        {
            followPlayer = true;
            currentHealth--;
            reduceHealthBar(currentHealth / maxHealth);

			if (UnityEngine.Random.value > .60 && !sound.isPlaying) {
				sound.volume = 1f;
				sound.pitch = UnityEngine.Random.Range (.75f, 1.25f);
				sound.Play ();
			}

            if (currentHealth <= 0)
            {
				GameObject port = Instantiate (portal);
				port.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 2, -1);
                Destroy(this.gameObject);
            }
        }
    }
    void reduceHealthBar(float remainingHealth)
    {
        healthBar.transform.localScale = new Vector3(remainingHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
