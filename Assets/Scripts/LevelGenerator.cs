using UnityEngine;
using System.Collections;
using SimpleJSON; // http://wiki.unity3d.com/index.php/SimpleJSON
using System;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{

    public TextAsset[] jsonText;

    public Sprite[] tiles; // add the sprites from the sheet in the unity editor

    List<GameObject> rooms = new List<GameObject>(); // Holds all the rooms

	public GameObject enemyPrefab;
	public GameObject slowPrefab;
	public GameObject damagePrefab;
	public GameObject bossPrefab;
	public GameObject speedPrefab;

	public GameObject player;
	public TextAsset startingRoomJson;
	public TextAsset bossRoom;

	private Dictionary<string, bool> roomsPlaced = new Dictionary<string, bool>();

	public int maxTries = 500;
	public static int RoomsToGenerate = 6;

    void Start()
    {
		GenerateLevel (RoomsToGenerate);
    }
    
    GameObject parseRoomJson(string roomFile)
    {
        JSONNode roomData = JSON.Parse(roomFile);
		//Debug.Log ("Making room " + roomData["layers"][0]["tiles"].ToString());
        
        GameObject room = new GameObject();
        room.AddComponent<Room>();
        room.transform.SetParent(this.transform);

		//Debug.Log ("Before loop " + roomData["layers"]["tiles"].AsObject.ToString());
		//Floor
		foreach (JSONNode tile in roomData["layers"][2]["tiles"].AsArray)
        {
			if (Int32.Parse (tile ["tile"]) >= 0) {
				//Debug.Log (tile.ToString());
				GameObject newTile = new GameObject ();
				newTile.transform.SetParent (room.transform);
				SpriteRenderer renderer = newTile.AddComponent<SpriteRenderer> ();
				renderer.sprite = tiles [Int32.Parse (tile ["tile"])];
				newTile.transform.position = new Vector3 (Int32.Parse (tile ["x"]), Int32.Parse (tile ["y"]));
				newTile.name = "floor_" + newTile.transform.position.x + "_" + newTile.transform.position.y;
			}
        }

		// walls
		foreach (JSONNode tile in roomData["layers"][1]["tiles"].AsArray) {
			if (Int32.Parse (tile ["tile"]) >= 0) {
				//Debug.Log (tile.ToString());
				GameObject newTile = new GameObject();
				newTile.transform.SetParent(room.transform);
				SpriteRenderer renderer = newTile.AddComponent<SpriteRenderer>();
				renderer.sprite = tiles[Int32.Parse(tile["tile"])];
				newTile.transform.position = new Vector3(Int32.Parse(tile["x"]), Int32.Parse(tile["y"]));
                newTile.tag = "Wall";
				newTile.name = "wall_" + newTile.transform.position.x + "_" + newTile.transform.position.y;
				renderer.sortingOrder = 1;
				BoxCollider boxCollider = newTile.AddComponent<BoxCollider> ();
				boxCollider.size = new Vector3 (1f, 1f, 2f);
				boxCollider.center = new Vector3 (0.5f, -0.5f, -1f);
			}
		}

		//Enemies
		foreach (JSONNode tile in roomData["layers"][0]["tiles"].AsArray) {
			int enemyType = Int32.Parse (tile ["tile"]);
			if (enemyType > 0) {
				GameObject enemy;
				switch (enemyType) {
				case 2: // Normal enemy
					enemy = Instantiate (enemyPrefab);
					enemy.GetComponent<FollowEnemy> ().player = player;
					room.GetComponent<Room> ().enemies.Add (enemy);
					break;
				case 3: // Slow trap
					enemy = Instantiate (slowPrefab);
					break;
				case 4: // Damage trap
					enemy = Instantiate (damagePrefab);
					break;
				case 5: // Speed trap
					enemy = Instantiate (speedPrefab);
					break;
				case 6: // Random enemy
					float rand = UnityEngine.Random.value;
					if (rand > .75f) {
						enemy = Instantiate (enemyPrefab);
					} else if (rand > .50f) {
						enemy = Instantiate (slowPrefab);
					} else if (rand > .25f) {
						enemy = Instantiate (damagePrefab);
					} else {
						enemy = Instantiate (speedPrefab);
					}
					break;
				case 7: // Random chance enemy
					float randChance = UnityEngine.Random.value;
					if (randChance > .66) {
						enemy = Instantiate (enemyPrefab);
						enemy.GetComponent<FollowEnemy> ().player = player;
						room.GetComponent<Room> ().enemies.Add (enemy);
					} else {
						enemy = null;
					}
					break;
				case 8: // Boss
					enemy = Instantiate (bossPrefab);
					room.GetComponent<Room> ().boss = enemy;
					break;
				default:
					enemy = Instantiate (enemyPrefab);
					room.GetComponent<Room> ().enemies.Add (enemy);
					enemy.GetComponent<FollowEnemy> ().player = player;
					break;
				}
				if (enemy != null) {
					enemy.transform.SetParent (room.transform);
					enemy.transform.position = new Vector3 (Int32.Parse (tile ["x"]), Int32.Parse (tile ["y"]), -1);
					enemy.name = "enemy_" + enemy.transform.position.x + "_" + enemy.transform.position.y;
				}
			}
		}
        return room;
    }

    void GenerateLevel(int numberOfRooms)
    {
        int roomsLeft = numberOfRooms;
		int tries = 0;
        while (roomsLeft > 0 && tries < maxTries)
        {
			tries++;
			if (roomsLeft == numberOfRooms) {
				// first room;
				GameObject firstRoom = parseRoomJson (startingRoomJson.text);
				firstRoom.name = "Room_" + roomsLeft;
				rooms.Add (firstRoom);
				roomsPlaced.Add (firstRoom.transform.position.x + "_" + firstRoom.transform.position.y, true);
				roomsLeft--;
			} else {
                rooms.Shuffle();
                string randExit = GetRandomExit();
				if (!rooms[0].GetComponent<Room>().HasExit(randExit) && !IsRoomTaken(randExit, rooms[0])) {
					GameObject newRoom;
					if (roomsLeft == 1) {
						newRoom = parseRoomJson (bossRoom.text);
					} else {
						newRoom = parseRoomJson(GetRandomRoom());
					}
                    // Set position based on exit
                    if (randExit == "e")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x + 25, rooms[0].transform.position.y);
						//Debug.Log ("Adding exit w");
						newRoom.GetComponent<Room>().addToDictionary("w", rooms[0]);

                    }
                    else if (randExit == "w")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x - 25, rooms[0].transform.position.y);
						//Debug.Log ("Adding exit e");
						newRoom.GetComponent<Room>().addToDictionary("e", rooms[0]);
                    }
                    else if (randExit == "n")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x, rooms[0].transform.position.y + 25);
						//Debug.Log ("Adding exit s");
						newRoom.GetComponent<Room>().addToDictionary("s", rooms[0]);
                    }
                    else {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x, rooms[0].transform.position.y -  25);
						//Debug.Log ("Adding exit n");
						newRoom.GetComponent<Room>().addToDictionary("n", rooms[0]);
                    }

					roomsPlaced.Add (newRoom.transform.position.x + "_" + newRoom.transform.position.y, true);
					Debug.Log("Added at " + newRoom.transform.position.x + "_" + newRoom.transform.position.y);

					//Debug.Log ("Adding exit " + randExit);
					rooms[0].GetComponent<Room>().addToDictionary(randExit, newRoom);

					newRoom.name = "Room_" + roomsLeft;

                    rooms.Add(newRoom);
                    roomsLeft--;

					DeleteWalls (randExit, rooms [0], newRoom);
                }
            }
        }
    }

	bool IsRoomTaken(string dir, GameObject oldRoom) {
		if (dir == "e") {
			Debug.Log("Room taken ? " + roomsPlaced.ContainsKey ((rooms [0].transform.position.x + 25).ToString () + "_" + rooms [0].transform.position.y.ToString ())); 
			return roomsPlaced.ContainsKey ((rooms [0].transform.position.x + 25) + "_" + rooms [0].transform.position.y);
		} else if (dir == "w") {
			Debug.Log("Room taken ? " + roomsPlaced.ContainsKey ((rooms [0].transform.position.x - 25).ToString () + "_" + rooms [0].transform.position.y.ToString ())); 
			return roomsPlaced.ContainsKey ((rooms [0].transform.position.x - 25) + "_" + rooms [0].transform.position.y);
		} else if (dir == "n") {
			Debug.Log("Room taken ? " + roomsPlaced.ContainsKey (rooms [0].transform.position.x.ToString () + "_" + (rooms [0].transform.position.y + 25).ToString ())); 
			return roomsPlaced.ContainsKey (rooms [0].transform.position.x+ "_" + (rooms [0].transform.position.y + 25));
		} else if (dir == "s") {
			Debug.Log("Room taken ? " + roomsPlaced.ContainsKey ((rooms [0].transform.position.x + 25).ToString () + "_" + (rooms [0].transform.position.y - 25).ToString ()));
			return roomsPlaced.ContainsKey (rooms [0].transform.position.x + "_" + (rooms [0].transform.position.y - 25));
		}

		return true;
	}

	void DeleteWalls(string dir, GameObject baseRoom, GameObject newRoom) {
		if (dir == "e") {
			// Base room
			Destroy (baseRoom.transform.FindChild ("wall_24_11").gameObject);
			Destroy (baseRoom.transform.FindChild ("wall_24_12").gameObject);
			Destroy (baseRoom.transform.FindChild ("wall_24_13").gameObject);
			//New Room
			Destroy (newRoom.transform.FindChild ("wall_0_11").gameObject);
			Destroy (newRoom.transform.FindChild ("wall_0_12").gameObject);
			Destroy (newRoom.transform.FindChild ("wall_0_13").gameObject);
		} else if (dir == "w") {
			// Base room
			Destroy (baseRoom.transform.FindChild ("wall_0_11").gameObject);
			Destroy (baseRoom.transform.FindChild ("wall_0_12").gameObject);
			Destroy (baseRoom.transform.FindChild ("wall_0_13").gameObject);
			//New Room
			Destroy (newRoom.transform.FindChild ("wall_24_11").gameObject);
			Destroy (newRoom.transform.FindChild ("wall_24_12").gameObject);
			Destroy (newRoom.transform.FindChild ("wall_24_13").gameObject);
		} else if (dir == "n") {
			// Base room
			Destroy (baseRoom.transform.FindChild ("wall_11_24").gameObject);
			Destroy (baseRoom.transform.FindChild ("wall_12_24").gameObject);
			Destroy (baseRoom.transform.FindChild ("wall_13_24").gameObject);
			//New Room
			Destroy (newRoom.transform.FindChild ("wall_11_0").gameObject);
			Destroy (newRoom.transform.FindChild ("wall_12_0").gameObject);
			Destroy (newRoom.transform.FindChild ("wall_13_0").gameObject);
		} else {
			// Base room
			Destroy (baseRoom.transform.FindChild ("wall_11_0").gameObject);
			Destroy (baseRoom.transform.FindChild ("wall_12_0").gameObject);
			Destroy (baseRoom.transform.FindChild ("wall_13_0").gameObject);
			//New Room
			Destroy (newRoom.transform.FindChild ("wall_11_24").gameObject);
			Destroy (newRoom.transform.FindChild ("wall_12_24").gameObject);
			Destroy (newRoom.transform.FindChild ("wall_13_24").gameObject);
		}
	}

    string GetRandomRoom()
    {
		jsonText.Shuffle();
		return jsonText [0].text;
    }
    
    string GetRandomExit()
    {
        // 2/3 chance to go horizontal
        float rand = UnityEngine.Random.value;
        if (rand > .66)
        {
            return "e";
        }
        else if (rand > .33)
        {
            return "w";
        }
        else if (rand > .165)
        {
            return "n";
        }

        return "s";
    }
}