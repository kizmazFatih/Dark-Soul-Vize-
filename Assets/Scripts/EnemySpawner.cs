using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    public GameObject enemyPrefab;

    private Vector3[] spawnPoints;

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


    void Start(){
        spawnPoints = new Vector3[4];

        spawnPoints[0] = new Vector3(86,1,53);
        spawnPoints[1] = new Vector3(15,1,53);
        spawnPoints[2] = new Vector3(50,1,86);
        spawnPoints[3] = new Vector3(50,1,15);
    }


    void Update()
    {


        if (spawnedEnemyCount < 5)
        {
            if (activeEnemyCount < 2)
            {
                Vector3 spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
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
