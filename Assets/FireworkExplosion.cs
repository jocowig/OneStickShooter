using UnityEngine;
using System.Collections;

public class FireworkExplosion : MonoBehaviour {
    public float explosionLife = 2f;
    private float elapsedTime = 0f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > explosionLife)
        {
            Destroy(this.gameObject);

        }
    }
}
