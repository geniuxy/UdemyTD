using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class WaveDetails
{
    public int basicEnemyCount;
    public int fastEnemyCount;
}

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private WaveDetails currentWave;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float spawnCoolDown;
    private float spawnTimer;

    private List<GameObject> enemiesToCreate;

    [Header("Enemy Prefabs")] 
    [SerializeField] private GameObject basicEnemy;

    [SerializeField] private GameObject fastEnemy;

    private void Start()
    {
        enemiesToCreate = NewEnemyWave();
    }

    private void Update()
    {
        if (enemiesToCreate.Count <= 0) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0.0f)
        {
            spawnTimer = spawnCoolDown;
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        GameObject randomEnemy = GetRandomEnemy();
        GameObject newEnemy = Instantiate(randomEnemy, respawnPoint.position, Quaternion.identity);
    }

    private GameObject GetRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemiesToCreate.Count);
        GameObject chosenEnemy = enemiesToCreate[randomIndex];

        enemiesToCreate.Remove(chosenEnemy);

        return chosenEnemy;
    }

    private List<GameObject> NewEnemyWave()
    {
        enemiesToCreate = new List<GameObject>();

        for (int i = 0; i < currentWave.basicEnemyCount; i++)
        {
            enemiesToCreate.Add(basicEnemy);
        }

        for (int i = 0; i < currentWave.fastEnemyCount; i++)
        {
            enemiesToCreate.Add(fastEnemy);
        }

        return enemiesToCreate;
    }
}