using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject[] bossPrefabs;
    //public float[] enemyWeights;
    //[SerializeField]private float[] enemyWeightsNormalized;
    
    /*
     * Spawns a random Boss
     */
    public void SpawnBoss()
    {
        //decide enemy to spawn
        int bossId = Random.Range(0, bossPrefabs.Length);

        Instantiate(bossPrefabs[bossId], transform.position, Quaternion.identity);

        //update enemy health based of wave number
    }
}
