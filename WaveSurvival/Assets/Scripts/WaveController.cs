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

    public GameObject shopDoorClose;
    public ShopController shopControl;
    public GameObject bossDoorClose;

    public BossRoom bossRoom;

    public int maxEnemiesSpawn = 20;
    public float spawnRate = 0.5f;
    public float waveBreakTime = 0.2f;
    public int goldPerWave = 20;

    public int shopWaves = 5;
    public int bossWaves = 20;

    private int spawnedEnemyCount = 0;
    private float nextSpawnTime = 0f;
    private int count = 0;
    private float nextWaveTime = 2f;

    private int waveCount = 0;
    private int enemiesDefeated = 0;

    private bool inWave = false;

    [Range(0, 1)] public float enemyDropRate = 0.5f;

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

    /*
     * used to tell if all spawned enemies have died
     */
    public void BossDied()
    {
        //open boss door
        bossDoorClose.SetActive(false);
        //activate wave button 
        waveStart.SetActive(true);
    }

    private void WaveEnded()
    {
        //wave ended
        inWave = false;
        nextWaveTime = Time.time + waveBreakTime;

        if(waveCount%shopWaves == 0)//shop every 5 waves
            SetShop(true);

        if (waveCount % bossWaves == 0)//boss every 20 waves
            SetBossRoom(true);
        else//enable wave start button unless it is boss time
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

        SetShop(false);
        SetBossRoom(false);

        nextWaveTime = Time.time + waveBreakTime;

        //increase wave count
        waveCount++;
        //update wave count UI
        waveCountText.text = "" + waveCount;

    }

    private void SetShop(bool val)
    {
        shopDoorClose.SetActive(!val);//open/close door to the shop
        if(val)
            shopControl.ShopRefresh();
    }

    private void SetBossRoom(bool val)
    {
        bossDoorClose.SetActive(!val);//if val == true then door closed->set unactive

        if (val)
        {
            bossRoom.NewBossSpawn();
            //remove wave button
        }

    }

    //wave count getter
    public int GetWaveCount()
    {
        return waveCount;
    }

    public float GetEnemyDropRate()
    {
        return enemyDropRate;
    }
}
