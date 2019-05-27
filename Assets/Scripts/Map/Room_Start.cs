using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Start : Room {

    public override void GenerateRoomArray()
    {
        GameObject spawnPoint = new GameObject();
        spawnPoint.transform.position = transform.position + new Vector3(roomWidth / 2, roomHeight / 3, 0f);
        spawnPoint.transform.parent = transform;
        spawnPoint.name = "PlayerSpawnPoint";
        spawnPoint.tag = "PlayerSpawnPoint";
    }

    public override void SpawnBorder()
    {
        // Spawn Border
        for (int y = 0; y <= roomHeight; y++)
        {
            for (int x = 0; x < roomWidth; x++)
            {
                if (y == 0 || y == roomHeight || x == 0)
                {
                    room[y, x] = 1;
                }
                else if (x == roomWidth - 1 && (y > roomHeight - 2 || y < 2))
                {
                    room[y, x] = 1;
                }
            }
        }
    }
}
