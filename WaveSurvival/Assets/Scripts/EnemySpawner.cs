using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public void SpawnEnemy(int waveNum)
    {
        //decide enemy to spawn

        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        //update enemy health based of wave number
    }
}
