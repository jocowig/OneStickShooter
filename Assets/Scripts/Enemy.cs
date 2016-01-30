using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject player;
    public float followTime = 1f;
    public float followSpeed = 2f;
    private float elapsedTime = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (player != null)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > followTime)
            {
                iTween.MoveUpdate(player, player.transform.position, followSpeed);
                elapsedTime = 0f;
            }
        }
	}
}
