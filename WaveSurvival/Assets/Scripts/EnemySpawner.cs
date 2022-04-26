using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float[] enemyWeights;
    [SerializeField]
    private float[] enemyWeightsNormalized;

    private void Awake()
    {
        enemyWeightsNormalized = new float[enemyWeights.Length];
        NormalizeWeights();
    }

    public void SpawnEnemy()
    {
        //decide enemy to spawn
        int enenmyId = GetEnemyIndex();
            //Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[enenmyId], transform.position, Quaternion.identity);

        //update enemy health based of wave number
    }

    private void NormalizeWeights()//makes the weights total to 1.0
    {
        float total = 0;
        foreach (float i in enemyWeights)
        {
            total += i;
        }
        for (int i = 0; i < enemyWeights.Length; i++)
        {
            enemyWeightsNormalized[i] = enemyWeights[i] / total;
        }
    }

    private int GetEnemyIndex()
    {
        float totalWeight = 0;
        foreach (float i in enemyWeights)
        {
            totalWeight += i;
        }

        float value = Random.Range(0f, totalWeight);
        float weightSum = 0f;
        int index;
        for (index = 0; index < enemyWeights.Length; index++)
        {
            weightSum += enemyWeights[index];
            if (value <= weightSum)
                return index;
        }

        return 0;//return 0 index if no index selected
    }
}
