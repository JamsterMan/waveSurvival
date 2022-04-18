﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public int maxEnemiesSpawn = 5;
    public float spawnRate = 0.5f;

    private int spawnedEnemyCount = 0;
    private float nextSpawnTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (spawnedEnemyCount < maxEnemiesSpawn)
        {
            if (Time.time > nextSpawnTime)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
                nextSpawnTime = Time.time + (1f / spawnRate);
                spawnedEnemyCount++;
            }
        }
    }
}
