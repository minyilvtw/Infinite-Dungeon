using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum roomType { roomStart = 1, roomCorridor = 2, roomLanding = 3, roomDrop = 4, roomEnd = 5, roomBoss = 6 }

public class MapGenerator : MonoBehaviour {

    public GameObject[] roomPrefabs;

    //public GameObject mapStartPoint;

    public int mapHeight;
    public int mapWidth;

    public int roomHeight;
    public int roomWidth;

    private int[,] map;

    private void Awake()
    {
        FindObjectOfType<LevelManager>().InitLevelData += GenerateMap;
    }

    public void GenerateMap() {
        

        map = new int[mapWidth, mapHeight];

        Debug.Log("Generating Map...");

        GenerateMapArray();
        SpawnMap();

        FindObjectOfType<LevelManager>().InitLevelData -= GenerateMap;
    }

    public void GenerateMapArray()
    {
        int randomSpawnHeight = Random.Range(0, mapHeight - 1);
        map[0, randomSpawnHeight] = (int)roomType.roomStart;

        int nowX = 1;
        int nowY = randomSpawnHeight;
        int prevX = 0;
        int prevY = nowY;
        map[nowX, nowY] = (int)roomType.roomCorridor;
        bool endFlag = false;

        while (!endFlag)
        {
            int direction = Random.Range(0, 3);
            if (direction == 0)
            { // MOVE DOWN
                nowY -= 1;
                if (nowY > 0 && map[prevX, prevY] == (int)roomType.roomCorridor)
                {
                    map[prevX, prevY] = (int)roomType.roomDrop;
                    map[nowX, nowY] = (int)roomType.roomLanding;
                }
                else
                {
                    direction = 2;
                    nowY += 1;
                }
            }
            else if (direction == 1)
            { // MOVE UP
                nowY += 1;
                if (nowY < mapHeight - 1 && map[prevX, prevY] == (int)roomType.roomCorridor)
                {
                    map[prevX, prevY] = (int)roomType.roomLanding;
                    map[nowX, nowY] = (int)roomType.roomDrop;
                }
                else
                {
                    direction = 2;
                    nowY -= 1;
                }
            }
            else if (direction == 2)
            { // MOVE RIGHT
                nowX += 1;
                if (nowX < mapWidth - 2)
                {
                    map[nowX, nowY] = (int)roomType.roomCorridor;
                }
                else
                {
                    map[nowX, nowY] = (int)roomType.roomEnd;
                    map[nowX + 1, nowY] = (int)roomType.roomBoss;
                    endFlag = true;
                }
            }
            prevX = nowX;
            prevY = nowY;
        }
    }
 
    void SpawnMap()
    {
        GameObject mapContainer = GameObject.Find("MapContainer");

        for (int y = 0; y < map.GetLength(1); y++)
        {
            bool flipflop = true;
            bool firstTurn = true;
            for (int x = 0; x < map.GetLength(0) - 1; x++)
            {
                if (map[x, y] != 0)
                {
                    GameObject room = GameObject.Instantiate(roomPrefabs[map[x, y]], mapContainer.transform.position + new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity, null);
                    room.transform.parent = mapContainer.transform;

                    if (map[x, y] == (int)roomType.roomDrop || map[x, y] == (int)roomType.roomLanding)
                    {
                        if (firstTurn) {
                            firstTurn = false;
                            
                            if (map[x, y] == (int)roomType.roomLanding && map[x -1,y] == 0) {
                                flipflop = false;
                            }else if (map[x, y] == (int)roomType.roomDrop && map[x -1,y] == 0)
                            {
                                flipflop = false;
                            }
                        }
                        
                        room.GetComponent<Room>().wallOnRight = flipflop;
                       
                        flipflop = !flipflop;
                    }
                    room.GetComponent<Room>().CreateRoom();
                }
            }
        }
        
        for(int y = 0; y < map.GetLength(1); y++)
        {
            int x = map.GetLength(0) - 1;
            if (map[x,y] != 0)
            {
                GameObject room = Instantiate(roomPrefabs[map[x, y]], mapContainer.transform.position + new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity, mapContainer.transform);
                room.GetComponent<Room>().CreateRoom();
                break;
            }

            /*else
            {
                Instantiate(roomPrefabs[map[x, y]], mapContainer.transform.position + new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity, mapContainer.transform);
                Instantiate(roomPrefabs[map[x, y]], mapContainer.transform.position + new Vector3((x + 1) * roomWidth, y * roomHeight, 0), Quaternion.identity, mapContainer.transform);
            }*/

        }
    }
}
