using UnityEngine;
using System.Collections;

public class FollowEnemy : MonoBehaviour {

    public GameObject player;
    public float followTime = 1f;
    public float followSpeed = 2f;
    private float elapsedTime = 0f;
    private float currentHealth;
    public float maxHealth;
    public GameObject healthBar;

	public bool followPlayer = false;

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
	    if (player != null && followPlayer)
        {
            elapsedTime += Time.deltaTime;
            //Debug.Log("elapesed time " + elapsedTime);
            if (elapsedTime > followTime)
            {
                iTween.MoveTo(this.gameObject, player.transform.position, followSpeed);
                elapsedTime = 0f;
               // Debug.Log("Following");
            }
        }
	}

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag.Equals("Firework"))
        {
			followPlayer = true;
            currentHealth--;
            reduceHealthBar(currentHealth / maxHealth);
            if(currentHealth<=0)
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
