using UnityEngine;
using System.Collections;
using SimpleJSON; // http://wiki.unity3d.com/index.php/SimpleJSON
using System;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{

    public TextAsset jsonText;

    Sprite[] tiles; // add the sprites from the sheet in the unity editor

    List<GameObject> rooms = new List<GameObject>(); // Holds all the rooms

    void Start()
    {
        jsonText = (TextAsset)Resources.Load("testLevel.json");
        // TODO: add sprite sheet
        tiles = Resources.LoadAll<Sprite>("rougelikeDungeon_transparent");
    }
    
    GameObject parseRoomJson(string roomFile)
    {
        JSONNode roomData = JSON.Parse(roomFile);
        
        GameObject room = new GameObject();
        room.AddComponent<Room>();
        room.transform.SetParent(this.transform);
        room.name = "Room";

        foreach (JSONNode tile in roomData["layers"]["tiles"].AsArray)
        {
            GameObject newTile = new GameObject();
            newTile.transform.SetParent(room.transform);
            SpriteRenderer renderer = newTile.AddComponent<SpriteRenderer>();
            renderer.sprite = tiles[Int32.Parse(tile["tile"])];
            newTile.transform.position = new Vector3(Int32.Parse(tile["x"]), Int32.Parse(tile["y"]));
            newTile.name = "tile_" + newTile.transform.position.x + "_" + newTile.transform.position.y;
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
                rooms.Add(parseRoomJson(GetRandomRoom()));
                roomsLeft--;
            }
            else {
                rooms.Shuffle();
                string randExit = GetRandomExit();
                if (!rooms[0].GetComponent<Room>().HasExit(GetRandomExit())) {
                    GameObject newRoom = parseRoomJson(GetRandomRoom());
                    // Set position based on exit
                    // TODO: Destory Doors
                    if (randExit == "e")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x + 25, rooms[0].transform.position.y);
                        newRoom.GetComponent<Room>().exits.Add("w", rooms[0].GetComponent<Room>());
                    }
                    else if (randExit == "w")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x - 25, rooms[0].transform.position.y);
                        newRoom.GetComponent<Room>().exits.Add("e", rooms[0].GetComponent<Room>());
                    }
                    else if (randExit == "n")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x, rooms[0].transform.position.y - 25);
                        newRoom.GetComponent<Room>().exits.Add("s", rooms[0].GetComponent<Room>());
                    }
                    else {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x, rooms[0].transform.position.y + 25);
                        newRoom.GetComponent<Room>().exits.Add("n", rooms[0].GetComponent<Room>());
                    }

                    rooms[0].GetComponent<Room>().exits.Add(randExit, newRoom.GetComponent<Room>());

                    rooms.Add(newRoom);
                    roomsLeft--;
                }
            }
        }
    }

    string GetRandomRoom()
    {
        //JSONNode levelData = JSON.Parse(jsonText.text);
        //JSONNode randomRoom = levelData.AsArray[UnityEngine.Random.Range(0, levelData.AsArray.Count)];
        // string roomJson = randomRoom.ToString();
        // return roomJson;
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