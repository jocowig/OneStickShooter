﻿using UnityEngine;
using System.Collections;
using SimpleJSON; // http://wiki.unity3d.com/index.php/SimpleJSON
using System;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{

    public TextAsset jsonText;

    public Sprite[] tiles; // add the sprites from the sheet in the unity editor

    List<GameObject> rooms = new List<GameObject>(); // Holds all the rooms

    void Start()
    {
        //jsonText = (TextAsset)Resources.Load("testLevel.json");
        // TODO: add sprite sheet
        //tiles = Resources.LoadAll<Sprite>("rougelikeDungeon_transparent");
		GenerateLevel (6);
    }
    
    GameObject parseRoomJson(string roomFile)
    {
        JSONNode roomData = JSON.Parse(roomFile);
		Debug.Log ("Making room " + roomData["layers"][0]["tiles"].ToString());
        
        GameObject room = new GameObject();
        room.AddComponent<Room>();
        room.transform.SetParent(this.transform);

		//Debug.Log ("Before loop " + roomData["layers"]["tiles"].AsObject.ToString());
		//Floor
		foreach (JSONNode tile in roomData["layers"][1]["tiles"].AsArray)
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
		foreach (JSONNode tile in roomData["layers"][0]["tiles"].AsArray) {
			if (Int32.Parse (tile ["tile"]) >= 0) {
				//Debug.Log (tile.ToString());
				GameObject newTile = new GameObject();
				newTile.transform.SetParent(room.transform);
				SpriteRenderer renderer = newTile.AddComponent<SpriteRenderer>();
				renderer.sprite = tiles[Int32.Parse(tile["tile"])];
				newTile.transform.position = new Vector3(Int32.Parse(tile["x"]), Int32.Parse(tile["y"]));
				newTile.name = "wall_" + newTile.transform.position.x + "_" + newTile.transform.position.y;
				renderer.sortingOrder = 1;
				newTile.AddComponent<BoxCollider> ().size = new Vector3(1, 1, 1);
			}
		}

        // Add Collision prefab to room

        // Add Trigger prefab to detect room entry
        return room;
    }

    void GenerateLevel(int numberOfRooms)
    {
        int roomsLeft = numberOfRooms;
        while (roomsLeft > 0)
        {
            if (roomsLeft == numberOfRooms)
            {
                // first room;
				GameObject firstRoom = parseRoomJson(GetRandomRoom());
				firstRoom.name = "Room_" + roomsLeft;
				rooms.Add(firstRoom);
                roomsLeft--;
            }
            else {
                rooms.Shuffle();
                string randExit = GetRandomExit();
                if (!rooms[0].GetComponent<Room>().HasExit(randExit)) {
                    GameObject newRoom = parseRoomJson(GetRandomRoom());
                    // Set position based on exit
                    // TODO: Destory Doors
                    if (randExit == "e")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x + 25, rooms[0].transform.position.y);
						Debug.Log ("Adding exit w");
						newRoom.GetComponent<Room>().addToDictionary("w", rooms[0]);

                    }
                    else if (randExit == "w")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x - 25, rooms[0].transform.position.y);
						Debug.Log ("Adding exit e");
						newRoom.GetComponent<Room>().addToDictionary("e", rooms[0]);
                    }
                    else if (randExit == "n")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x, rooms[0].transform.position.y + 25);
						Debug.Log ("Adding exit s");
						newRoom.GetComponent<Room>().addToDictionary("s", rooms[0]);
                    }
                    else {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x, rooms[0].transform.position.y -  25);
						Debug.Log ("Adding exit n");
						newRoom.GetComponent<Room>().addToDictionary("n", rooms[0]);
                    }

					Debug.Log ("Adding exit " + randExit);
					rooms[0].GetComponent<Room>().addToDictionary(randExit, newRoom);

					newRoom.name = "Room_" + roomsLeft;

                    rooms.Add(newRoom);
                    roomsLeft--;

					DeleteWalls (randExit, rooms [0], newRoom);
                }
            }
        }
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
        //JSONNode levelData = JSON.Parse(jsonText.text);
        //JSONNode randomRoom = levelData.AsArray[UnityEngine.Random.Range(0, levelData.AsArray.Count)];
        // string roomJson = randomRoom.ToString();
        // return roomJson;
		//Debug.Log(jsonText.text);
        return jsonText.text;
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