using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {
	
	public Dictionary<string, Room> exits;
	
	public bool HasExit(string exit){
		return exits.ContainsKey(exit);
	}
}