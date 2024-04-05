using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    public GameObject enemyPrefab;

    public bool isDead = false;

    public int spawnedEnemyCount = 0;
    public int deadEnemyCount = 0;
    public int activeEnemyCount;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }


    void Update()
    {


        if (spawnedEnemyCount < 5)
        {
            if (activeEnemyCount < 2)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(-24, 24), 1, Random.Range(-24, 24));
                Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

                activeEnemyCount += 1;
                spawnedEnemyCount += 1;
            }
        }


    }

    public void DeathCounter()
    {
        deadEnemyCount += 1;
        activeEnemyCount -= 1;
    }
}
