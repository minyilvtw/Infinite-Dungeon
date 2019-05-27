using UnityEngine;

public class Room : MonoBehaviour {

    public int[,] room;

    public GameObject block;
    public GameObject platform;

    protected int roomWidth; // no less than 12
    protected int roomHeight; //more than 8

    public bool wallOnRight;

    public void CreateRoom()
    {
        roomHeight = GameObject.Find("GameManager").GetComponent<MapGenerator>().roomHeight;
        roomWidth = GameObject.Find("GameManager").GetComponent<MapGenerator>().roomWidth;
        SizeModify();

        room = new int[roomHeight + 1, roomWidth + 1];

        GenerateRoomArray();
        SpawnBorder();
        SpawnRoomTiles();    
    }

    public virtual void SizeModify() { }
    public virtual void GenerateRoomArray() { }
    public virtual void SpawnBorder() { }

    public void SpawnRoomTiles()
    {
        int spawnCount = 1;
        for (int y = 0; y <= roomHeight; ++y)
        {
            for (int x = 0; x <= roomWidth; ++x)
            {
                if (room[y, x] == 1)
                {
                    Instantiate(block, transform.position + new Vector3(x, y, 0), Quaternion.identity, transform);
                }

                else if (room[y, x] == 2)
                {
                    Instantiate(platform, transform.position + new Vector3(x, y, 0), Quaternion.identity, transform);
                    if (spawnCount > 0 && Random.Range(0, 2) == 0)
                    {
                        spawnCount--;
                        GameObject spawnPoint = new GameObject();
                        spawnPoint.transform.position = transform.position + new Vector3(x, y + 1, 0);
                        spawnPoint.transform.parent = transform;
                        spawnPoint.name = "EnemySpawnPoint";
                        spawnPoint.tag = "EnemySpawnPoint";
                    }
                }
            }
        }
    }
}
