using UnityEngine;
using System.Collections;
using SimpleJson; // http://wiki.unity3d.com/index.php/SimpleJSON

public class LevelGenerator : MonoBehaviour
{

    Sprite[] tiles; // add the sprites from the sheet in the unity editor

    GameObject[] rooms; // Holds all the rooms

    void Start()
    {
        // TODO: add sprite sheet
        tiles = Resources.LoadAll<Sprite>("spritesheet");
    }
    /*
    GameObject parseRoomJson(string roomFile)
    {
        JSONNode roomData = JSON.Parse(roomFile);

        GameObject room = new GameObject();
        room.AddComponent<Room>();
        room.transform.SetParent(this.transform);
        room.name = "Room";

        foreach (JSONNode tile in roomData["layers"]["tiles"])
        {
            GameObject tile = new GameObject();
            tile.transform.SetParent(room.transform);
            SpriteRenderer renderer = tile.AddComponent<SpriteRenderer>();
            renderer.sprite = tiles[Int32.Parse(tile["tile"])];
            tile.transform.position = new Vector3(Int32.Parse(tile["x"]), Int32.Parse(tile["y"]));
            tile.name = "tile_" + tile.transform.position.x + "_" + tile.transform.position.y;
        }

        // Add Collision prefab to room

        // Add Trigger prefab to detect room entry
    }

    void GenerateLevel(int numberOfRooms)
    {
        int roomsLeft = numberOfRooms;
        while (roomsLeft > 0)
        {
            if (roomsLeft == numberOfRooms)
            {
                // first room;
                rooms.Push(parseRoom(GetRandomRoom()));
                roomsLeft--;
            }
            else {
                rooms.Shuffle();
                string randExit = GetRandomExit();
                if (!rooms[0].GetComponent<Room>().HasExit(GetRandomExit()))) {
                    GameObject newRoom = parseRoom(GetRandomRoom());
                    // Set position based on exit
                    // TODO: Destory Doors
                    if (randExit == "e")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x + 25, rooms[0].transform.position.y);
                        newRoom.GetComponent<Room>().exits.Add("w", rooms[0]);
                    }
                    else if (randExit == "w")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x - 25, rooms[0].transform.position.y);
                        newRoom.GetComponent<Room>().exits.Add("e", rooms[0]);
                    }
                    else if (randExit == "n")
                    {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x, rooms[0].transform.position.y - 25);
                        newRoom.GetComponent<Room>().exits.Add("s", rooms[0]);
                    }
                    else {
                        newRoom.transform.position = new Vector3(rooms[0].transform.position.x, rooms[0].transform.position.y + 25);
                        newRoom.GetComponent<Room>().exits.Add("n", rooms[0]);
                    }

                    rooms[0].GetComponent<Room>().exits.Add(randExit, newRoom);

                    rooms.Push(newRoom);
                    roomsLeft--;
                }
            }
        }
    }

    string GetRandomRoom()
    {
        //TODO: set json file
        return roomJson;
    }
    */
    string GetRandomExit()
    {
        // 2/3 chance to go horizontal
        float rand = Random.value;
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