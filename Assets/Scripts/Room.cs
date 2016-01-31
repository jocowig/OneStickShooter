using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {
	
	public Dictionary<string, Room> exits = new Dictionary<string, Room>();
	
	public bool HasExit(string exit){
		Debug.Log(exits.ContainsKey(exit));
		return exits.ContainsKey(exit);
	}
}