using UnityEngine;

public class Room_End : Room {
    
    public override void GenerateRoomArray()
    {
        for(int i = -1; i < 2; ++i)
        {
            GameObject spawnPoint = new GameObject();
            spawnPoint.transform.position = transform.position + new Vector3(roomWidth / 2 + i * 2, 1, 0);
            spawnPoint.transform.parent = transform;
            spawnPoint.name = "ItemSpawnPoint";
            spawnPoint.tag = "ItemSpawnPoint";
        }        
    }

    public override void SpawnBorder()
    {
        // Spawn Border
        for (int y = 0; y <= roomHeight; y++)
        {
            for (int x = 0; x < roomWidth; x++)
            {
                if (y == 0 || y == roomHeight)
                {
                    room[y, x] = 1;
                }
                else if (x == 0 && (y > roomHeight - 2 || y < 2))
                {
                    room[y, x] = 1;
                }
            }
        }
    }
}
