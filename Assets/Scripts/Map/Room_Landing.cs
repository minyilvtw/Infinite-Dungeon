using UnityEngine;

public class Room_Landing : Room {

    bool build;

    public override void GenerateRoomArray()
    {
        int level = roomHeight;
        while (level > 1)
        {
            int i = 2 + level / 4;
            build = (level / 2) % 2 == 1;

            int maxWidth = Mathf.Min(4, roomWidth / 4);

            while (i < roomWidth - level / 4 - 2)
            {
                int count = 0;
                if (build)
                {
                    if (room[level - 2, i] != 2)
                    {
                        room[level, i] = 2;
                    }
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

            level -= 2;
        }
    }

    public override void SpawnBorder()
    {
        int wallSide = 0;
        if (wallOnRight)
        {
            wallSide = roomWidth - 1;
        }
        // Spawn Border
        for (int y = 0; y <= roomHeight; y++)
        {
            for (int x = 0; x < roomWidth; x++)
            {
                if (y == 0 || x == wallSide)
                {
                    room[y, x] = 1;
                }
            }
        }
    }
}
