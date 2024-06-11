using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int totalEnemies = 1;
    public bool enemyDead = false;

    void Start()
    {
        for (int i = 1; i < 9; i++)
        {
            SpawnEnemy();
            totalEnemies++;
        }
    }

    void Update()
    {
        if (totalEnemies != 100)
        {
            if (PlayerAlive.playerList.Length <= 5)
            {
                for (int i = 0; i < 4; i++)
                {
                    SpawnEnemy();
                    totalEnemies++;
                }
            }
        }
    }

    public Vector3 RandomSpawn()
    {
        float xLocation = Random.Range(-90, 60);
        float yLocation = 0.5f;
        float zLocation = Random.Range(-65, 50);

        Vector3 spawnLocation = new Vector3(xLocation, yLocation, zLocation);
        return spawnLocation;
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, RandomSpawn(), Quaternion.identity);
        enemy.name = "Enemy";
    }
}
