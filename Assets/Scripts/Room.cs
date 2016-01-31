using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

	public List<string> keys = new List<string>();
	public List<GameObject> values = new List<GameObject>();
	private Dictionary<string, GameObject> exits = new Dictionary<string, GameObject>();

	public List<GameObject> enemies = new List<GameObject> ();

	public GameObject boss;

	public GameObject cover;
	
	public bool HasExit(string exit){
		//Debug.Log(exits.ContainsKey(exit));
		return exits.ContainsKey(exit);
	}

	public void addToDictionary(string exit, GameObject room) {
		keys.Add (exit);
		values.Add (room);
		exits.Add (exit, room);
	}

	void Start() {
		BoxCollider boxColl = gameObject.AddComponent<BoxCollider> ();
		boxColl.isTrigger = true;
		boxColl.size = new Vector3 (25, 25, 2);
		boxColl.center = new Vector3 (12.5f, 12.5f, -1);

		cover = Instantiate (Resources.Load("Cover")) as GameObject;
		cover.transform.SetParent (this.transform);
	}

	void Update() {
		cover.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 24, -2);
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Trap Collision Entered");
		if (other.tag.Equals("Player"))
		{
			cover.transform.GetComponent<SpriteRenderer> ().enabled = false;

			foreach (GameObject enemy in enemies) {
				enemy.GetComponent<FollowEnemy> ().followPlayer = true;
			}

			if (boss != null) {
				boss.GetComponent<BossEnemy> ().followPlayer = true;
			}
		}
	}
}