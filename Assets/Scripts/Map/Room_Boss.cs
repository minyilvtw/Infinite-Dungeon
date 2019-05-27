using UnityEngine;

public class Room_Boss : Room {

    public GameObject trigger;

    public override void SizeModify()
    {
        roomWidth = roomWidth * 2 - 5;
    }

    public override void GenerateRoomArray()
    {
        for (int i = 1; i < roomHeight; ++i)
        {
            Instantiate(trigger, transform.position + new Vector3(2, i, 0), Quaternion.identity, transform);
        }
    }

    public int rw
    {
        get
        {
            return roomWidth;
        }
    }

    public int rh
    {
        get
        {
            return roomHeight;
        }
    }
    
    public override void SpawnBorder()
    {
        // Spawn Border
        for (int y = 0; y <= roomHeight; y++)
        {
            for (int x = 0; x < roomWidth; x++)
            {
                if (y == 0 || y == roomHeight || x == roomWidth - 1)
                {
                    room[y, x] = 1;
                }
            }
        }
    }
}
