using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public EnemySpawner[] spawners = new EnemySpawner[4];

    public int maxEnemiesSpawn = 20;
    public float spawnRate = 0.5f;
    public float waveBreakTime = 2f;

    private int spawnedEnemyCount = 0;
    private float nextSpawnTime = 0f;
    private int count = 0;
    private float nextWaveTime = 2f;

    private int enemiesDefeated = 0;

    private bool inWave = true;

    // Update is called once per frame
    void Update()
    {
        if (inWave)
        {
            if (spawnedEnemyCount < maxEnemiesSpawn)
            {
                if (Time.time > nextSpawnTime)
                {
                    foreach (EnemySpawner spawn in spawners)
                    {
                        spawn.SpawnEnemy();
                        count++;
                    }
                    nextSpawnTime = Time.time + (1f / spawnRate);
                    spawnedEnemyCount += count;
                    count = 0;
                }
            }
        }
        else
        {
            if(Time.time > nextWaveTime)
            {
                spawnedEnemyCount = 0;

                inWave = true;
                Debug.Log("New wave start");
            }
        }
    }

    public void EnemyDied()
    {
        enemiesDefeated++;
        if(enemiesDefeated == maxEnemiesSpawn)
        {
            //wave ended
            inWave = false;
            nextWaveTime = Time.time + waveBreakTime;
            Debug.Log("Wave ended");

            //enable wave start button
        }
    }

}
