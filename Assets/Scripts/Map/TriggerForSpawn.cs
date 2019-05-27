using UnityEngine;

public class TriggerForSpawn : MonoBehaviour {

    public GameObject block;
    public GameObject Boss;
    GameObject bossRoom;
    
    void OnTriggerEnter2D()
    {
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Enemy")) {
            Destroy(x);
        }

        bossRoom = GameObject.FindGameObjectWithTag("Finish");
        Vector3 position = bossRoom.transform.position;
        int height = bossRoom.GetComponent<Room_Boss>().rh;
        int width = bossRoom.GetComponent<Room_Boss>().rw;
        for(int i = 1; i < height; ++i)
        {
            Instantiate(block, position + new Vector3(0, i, 0), Quaternion.identity, transform.parent);
        }

        GameObject obj = Instantiate(Boss, position + new Vector3(width / 2, height / 2, 0), Quaternion.identity, transform.parent);
        obj.GetComponent<EnemyStatus>().level = FindObjectOfType<LevelManager>().level;

        GameObject[] triggers = GameObject.FindGameObjectsWithTag("Trigger");
        foreach(GameObject trigger in triggers)
        {
            Destroy(trigger.gameObject);
        }       
    }
}
