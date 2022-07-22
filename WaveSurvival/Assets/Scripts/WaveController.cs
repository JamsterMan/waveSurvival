using UnityEngine;
using UnityEngine.SceneManagement;
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

    private int _spawnedEnemyCount = 0;
    private float _nextSpawnTime = 0f;
    private int _count = 0;
    private float _nextWaveTime = 2f;

    private int _waveCount = 0;
    private int _bossCount = 0;
    private int _enemiesDefeated = 0;

    private bool _inWave = false;
    private bool _finalBossFight = false;

    [Range(0, 1)] public float enemyDropRate = 0.5f;

    //wave count getter
    public int GetWaveCount(){ return _waveCount; }
    public int GetBossCount(){ return _bossCount; }

    // Update is called once per frame
    void Update()
    {
        if (_inWave && Time.time > _nextWaveTime)
        {
            if (_spawnedEnemyCount < maxEnemiesSpawn)
            {
                if (Time.time > _nextSpawnTime)
                {
                    foreach (EnemySpawner spawn in spawners)
                    {
                        spawn.SpawnEnemy();
                        _count++;
                    }
                    _nextSpawnTime = Time.time + (1f / spawnRate);
                    _spawnedEnemyCount += _count;
                    _count = 0;
                }
            }
        }
    }

    /*
     * used to tell if all spawned enemies have died
     */
    public void EnemyDied()
    {
        _enemiesDefeated++;
        if(_enemiesDefeated == maxEnemiesSpawn)
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
        if (!_finalBossFight)
        {
            //open boss door
            bossDoorClose.SetActive(false);
            //activate wave button 
            waveStart.SetActive(true);
            _bossCount++;
        }
        else
        {
            //game won
            //add end game screen / animation
            SceneManager.LoadScene(0);
        }
    }

    /*
     * sets up arena and doors for inbetween waves
     */
    private void WaveEnded()
    {
        //wave ended
        _inWave = false;
        _nextWaveTime = Time.time + waveBreakTime;

        if(_waveCount % shopWaves == 0)//shop every 5 waves
            SetShop(true);

        if (_waveCount % bossWaves == 0)//boss every 20 waves
            SetBossRoom(true);
        else//enable wave start button unless it is boss time
            waveStart.SetActive(true);

        if (_waveCount % 100 == 0)
            _finalBossFight = true;

        playerGold.AddGold(goldPerWave);
    }

    /*
     * Sets starting values for a new wave
     * also updates UI
     */
    public void WaveStart()
    {
        _spawnedEnemyCount = 0;
        _enemiesDefeated = 0;

        _inWave = true;

        SetShop(false);
        SetBossRoom(false);

        _nextWaveTime = Time.time + waveBreakTime;

        //increase wave count
        _waveCount++;
        //update wave count UI
        waveCountText.text = "" + _waveCount;

    }

    //sets up the shop room with new items
    private void SetShop(bool val)
    {
        shopDoorClose.SetActive(!val);//open/close door to the shop, since this is for the door close sprite it needs the opposite bool of val
        if(val)
            shopControl.ShopRefresh();
    }

    //sets up the boss room for a boss fight
    private void SetBossRoom(bool val)
    {
        bossDoorClose.SetActive(!val);//if val == true then door closed->set unactive, since this is for the door close sprite it needs the opposite bool of val

        if (val)
        {
            bossRoom.NewBossSpawn();
        }

    }

    public float GetEnemyDropRate()
    {
        return enemyDropRate;
    }
}
