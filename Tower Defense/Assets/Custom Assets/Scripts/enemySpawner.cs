using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Text timerText;
    private int enemiesInWave = 1;
    private float spawnWaveTime = 5f;
    private float spawnEnemyTime = 1f;
    private float timer = 5f;
    private bool isWaveSpawning = false;

    private void Update()
    {
        if (timer <= 0)
        {
            isWaveSpawning = true;
            timerText.text = "Spawning Wave";
            StartCoroutine(spawnWave());
            timer = spawnWaveTime;
        }
        if (!isWaveSpawning)
        {
            timer -= Time.deltaTime;
            timerText.text = "Next Wave:  " + ((int)timer).ToString();
        }
        
    }
    IEnumerator spawnWave()
    {
        enemiesInWave++;
        for (int i = 0; i < enemiesInWave; i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(spawnEnemyTime);
        }
        isWaveSpawning = false;
    }

    private void spawnEnemy()
    {
        Instantiate(enemyPrefab, transform);
    }
}
