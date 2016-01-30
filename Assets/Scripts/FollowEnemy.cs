using UnityEngine;
using System.Collections;

public class FollowEnemy : MonoBehaviour {

    public GameObject player;
    public float followTime = 1f;
    public float followSpeed = 2f;
    public int health;
    private float elapsedTime = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (player != null)
        {
            elapsedTime += Time.deltaTime;
            //Debug.Log("elapesed time " + elapsedTime);
            if (elapsedTime > followTime)
            {
                iTween.MoveTo(this.gameObject, player.transform.position, followSpeed);
                elapsedTime = 0f;
                Debug.Log("Following");
            }
        }
	}

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag.Equals("Firework"))
        {
            health--;
            if(health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
