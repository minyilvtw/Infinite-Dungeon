using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject[] itemPrefabs;
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;

    private GameObject playerSpawnPoint;
    private List<GameObject> itemSpawnPoints;
    private List<GameObject> enemySpawnPoints;
    private List<GameObject> bossSpawnPoints;

    private void Awake()
    {
        FindObjectOfType<LevelManager>().InitLevelData += Spawn;
    }

    public void Spawn()
    {
        Debug.Log("Spawning Players, Enemies...");

        int enemiesCount = FindObjectOfType<LevelManager>().spawnCount;
        int level = FindObjectOfType<LevelManager>().level;

        playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
        enemySpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemySpawnPoint"));
        itemSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("ItemSpawnPoint"));
    

        // Player
        if(GameObject.FindGameObjectWithTag("Player") != null){
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }

        GameObject.Instantiate(playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity, null);

        // Enemies
        for (int i = 0; i < enemiesCount; i++)
        {
            int randomPoint = Random.Range(0, enemySpawnPoints.Count);
            int randomType = Random.Range(0, enemyPrefabs.Length);
            GameObject obj = GameObject.Instantiate(enemyPrefabs[randomType], enemySpawnPoints[randomPoint].transform.position, Quaternion.identity, null);
            obj.GetComponent<EnemyStatus>().level = level;
        }

        // Item
        for (int i = 0; i < 3; i++)
        {
            int randomType = Random.Range(0, itemPrefabs.Length);
            
            GameObject obj = GameObject.Instantiate(itemPrefabs[randomType], itemSpawnPoints[i].transform.position, Quaternion.identity, null);
        }


        FindObjectOfType<LevelManager>().InitLevelData -= Spawn;

    }
}
