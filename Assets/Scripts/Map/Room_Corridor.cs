using UnityEngine;

public class Room_Corridor : Room {

    bool build;

    public override void GenerateRoomArray() {
        int level = 0;
        while (level < roomHeight - 3)
        {
            level += 2;

            int i = 2 + level / 4;
            build = (level / 2) % 2 == 1;

            int maxWidth = Mathf.Min(4, roomWidth / 4);

            while (i < roomWidth - level / 4 - 2)
            {
                int count = 0;
                if (build)
                {
                    room[level, i] = 2;
                    ++i;
                    do
                    {
                        if (room[level - 2, i] != 2)
                        {
                            room[level, i] = 2;
                            ++count;
                        }
                        ++i;
                    } while (Random.Range(0, 2) == 0 && count < maxWidth && i < roomWidth - level / 4 - 2);
                }
                else
                {
                    do
                    {
                        ++i;
                        ++count;
                    } while (Random.Range(0, 2) == 0 && count < maxWidth && i < roomWidth - level / 4 - 2);
                }
                build = !build;
            }
            
            if (Random.Range(0, 2) != 0)
            {
                break;
            }
        }
    }

    public override void SpawnBorder()
    {
        // Spawn Border
        for (int y = 0; y <= roomHeight; y++)
        {
            for (int x = 0 ; x < roomWidth; x++)
            {
                if (y == 0 || y == roomHeight) {
                    room[y, x] = 1;
                }                   
            }
        }
    }
}
    
