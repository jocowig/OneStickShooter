using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

	public List<string> keys = new List<string>();
	public List<GameObject> values = new List<GameObject>();
	private Dictionary<string, GameObject> exits = new Dictionary<string, GameObject>();

	public List<GameObject> enemies = new List<GameObject> ();
	
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
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Trap Collision Entered");
		if (other.tag.Equals("Player"))
		{
			foreach (GameObject enemy in enemies) {
				enemy.GetComponent<FollowEnemy> ().followPlayer = true;
			}
		}
	}
}