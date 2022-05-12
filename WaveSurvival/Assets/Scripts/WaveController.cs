using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    public EnemySpawner[] spawners = new EnemySpawner[4];
    public GameObject waveStart;
    public Text waveCountText;
    public PlayerGold playerGold;

    public int maxEnemiesSpawn = 20;
    public float spawnRate = 0.5f;
    public float waveBreakTime = 0.2f;
    public int goldPerWave = 20;

    private int spawnedEnemyCount = 0;
    private float nextSpawnTime = 0f;
    private int count = 0;
    private float nextWaveTime = 2f;

    private int waveCount = 0;
    private int enemiesDefeated = 0;

    private bool inWave = false;

    // Update is called once per frame
    void Update()
    {
        if (inWave && Time.time > nextWaveTime)
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
    }

    /*
     * used to tell if all spawned enemies have died
     */
    public void EnemyDied()
    {
        enemiesDefeated++;
        if(enemiesDefeated == maxEnemiesSpawn)
        {
            //wave ended
            WaveEnded();
        }
    }

    private void WaveEnded()
    {
        //wave ended
        inWave = false;
        nextWaveTime = Time.time + waveBreakTime;
        Debug.Log("Wave ended");

        //enable wave start button
        waveStart.SetActive(true);
        playerGold.AddGold(goldPerWave);
    }

    /*
     * Sets starting values for a new wave
     * also updates UI
     */
    public void WaveStart()
    {
        spawnedEnemyCount = 0;
        enemiesDefeated = 0;

        inWave = true;
        Debug.Log("new wave started");
        nextWaveTime = Time.time + waveBreakTime;

        //increase wave count
        waveCount++;
        //update wave count UI
        waveCountText.text = "" + waveCount;

    }

    //wave count getter
    public int GetWaveCount()
    {
        return waveCount;
    }

}
