using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {

    public static float speed = 1f;
    public GameObject player;
    private float currentHealth;
    public float maxHealth;
    public GameObject healthBar;

    public bool followPlayer = false;

	public GameObject portal;

    // Use this for initialization
    void Start()
    {
    currentHealth = maxHealth;
    GetComponent<Rigidbody>().velocity = new Vector3(speed, speed);
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
            if (currentHealth <= 0)
            {
				GameObject port = Instantiate (portal);
				PlayerScript.bossesKilled++;
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
