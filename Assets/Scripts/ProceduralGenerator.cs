//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public enum roomType { roomStart = 1, roomCorridor = 2, roomLanding = 3, roomDrop = 4, roomEnd = 5, roomBoss = 6}

//public class ProceduralGenerator : MonoBehaviour {


//    public GameObject[] rooms;

//    private GameObject mapStartPoint;

//    public int mapHeight;
//    public int mapWidth;

//    public int roomHeight;
//    public int roomWidth;
    
//    private int[,] map;

//    public void Start() {

//        map = new int[mapWidth, mapHeight];

//        InitializeArray();
//        GenerateMapArray();
//        PrintDebugRoom();
//        SpawnMap();
//    }

//    public void GenerateMapArray() {

//        int randomSpawnHeight = Random.Range(0, mapHeight - 1);
//        map[0, randomSpawnHeight] = (int)roomType.roomStart;

//        int nowX = 1;
//        int nowY = randomSpawnHeight;
//        int prevX = 1;
//        int prevY = nowY;

//        map[nowX, nowY] = (int)roomType.roomCorridor;

//        bool endFlag = false;
//        while (!endFlag) {

//            int direction = Random.Range(0, 2);

//            if (direction == 0) { // MOVE DOWN
//                nowY -= 1;
//                if (nowY > 0 && map[prevX,prevY] == (int) roomType.roomCorridor)
//                {
//                    map[prevX, prevY] = (int) roomType.roomDrop;
//                    map[nowX, nowY] = (int)roomType.roomLanding;

//                }
//                else
//                {
//                    direction = 2;
//                    nowY += 1;
//                }
//            }

//            if (direction == 1)
//            { // MOVE UP
//                nowY += 1;
//                if (nowY < mapHeight - 1 && map[prevX, prevY] == (int)roomType.roomCorridor)
//                {
//                    map[prevX, prevY] = (int)roomType.roomLanding;
//                    map[nowX, nowY] = (int)roomType.roomDrop;
                    
//                }
//                else
//                {
//                    direction = 2;
//                    nowY -= 1;
//                }
//            }

//            if (direction == 2 || direction == 3) { // MOVE RIGHT
//                     nowX += 1;
//                     if (nowX < mapWidth - 2)
//                     {
//                         map[nowX, nowY] = (int)roomType.roomCorridor;
//                     }
//                     else {
//                         map[nowX, nowY] = (int)roomType.roomEnd;
//                         map[nowX + 1, nowY] = (int)roomType.roomBoss;
//                         endFlag = true;
//                     }
//            }

//            prevX = nowX;
//            prevY = nowY;

//        }




//    }

//    void InitializeArray() {

//        for (int i = 0; i < map.GetLength(1); i++)
//        {
//            for (int j = 0; j < map.GetLength(0); j++)
//            {
//                map[j, i]= 0;
//            }

//        }
			

//    }

//    void PrintDebugRoom()
//    {
//        string p = "";

//        for (int i = map.GetLength(1) - 1; i >= 0; i--)
//        {
//            for (int j = 0; j < map.GetLength(0); j++)
//            {
//                p += map[j, i] + " ";
//            }
//            p += "\n";
//        }

//        print(p);
//    }

//    void SpawnMap() {

//        for (int i = 0; i < map.GetLength(1); i++)
//        {
//            for (int j = 0; j < map.GetLength(0); j++)
//            {
//                GameObject spawn = GameObject.Instantiate(rooms[map[j, i]], this.transform.position + new Vector3(j * roomWidth, i * roomHeight, 0), Quaternion.identity, null);
//                //spawn.transform.SetParent(mapStartPoint.transform);
//            }

//        }
//    }
//}
