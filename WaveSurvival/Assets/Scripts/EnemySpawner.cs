using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject normalEnemyPrefab;
    public GameObject fastEnemyPrefab;
    public GameObject slowEnemyPrefab;

    public void SpawnEnemy(int waveNum)
    {
        //decide enemy to spawn
        GameObject enemy;
        if(waveNum % 3 == 1)
            enemy = normalEnemyPrefab;
        else if(waveNum % 3 == 2)
            enemy = fastEnemyPrefab;
        else
            enemy = slowEnemyPrefab;


        Instantiate(enemy, transform.position, Quaternion.identity);

        //update enemy health based of wave number
    }
}
