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
}