using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {

    public float speed = 1f;
    public GameObject player;
    private float elapsedTime = 0f;
    private float currentHealth;
    public float maxHealth;
    public GameObject healthBar;

    public bool followPlayer = false;

    // Use this for initialization
    void Start()
    {
    currentHealth = maxHealth;
    GetComponent<Rigidbody>().velocity = new Vector3(speed, speed);
	}

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            Vector3 norm = GetComponent<Rigidbody>().velocity.normalized;

            norm.z = 0;

            GetComponent<Rigidbody>().velocity = norm * speed;
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
                Destroy(this.gameObject);
            }
        }
    }
    void reduceHealthBar(float remainingHealth)
    {
        healthBar.transform.localScale = new Vector3(remainingHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
