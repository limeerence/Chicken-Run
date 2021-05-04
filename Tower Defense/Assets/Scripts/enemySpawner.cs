using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] private gameController controller;
    [SerializeField] private GameObject[] enemyPrefabs;
    
    private int enemiesInWave = 1;
    private int tank = 9;
    private int fast = 19;
    private float spawnWaveTime = 5f;
    private float spawnEnemyTime = 1.5f;
    private float timer = 5f;
    private bool isWaveSpawning = false;
    private bool isNextRound = false;

    private void Start()
    {
        if (!controller)
        {
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        }
    }

    private void Update()
    {
        if (timer <= 0 && controller.wavesRemaining > 0)
        {
            isNextRound = false;
            isWaveSpawning = true;
            controller.thisWave++;
            StartCoroutine(spawnWave());
            timer = spawnWaveTime;
        }
        if (!isWaveSpawning && controller.wavesRemaining > 0)
        {
            timer -= Time.deltaTime;
        }
        if (controller.wavesRemaining == 0 && !isNextRound)
        {
            isNextRound = true;
            enemiesInWave = 1;
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
        controller.wavesRemaining--;
        isWaveSpawning = false;
    }

    private void spawnEnemy()
    {
        int enemyVariant = UnityEngine.Random.Range(0, 99);
        switch (enemyVariant)
        {
            case int n when (n <= tank):
                Instantiate(enemyPrefabs[0], transform);
                break;

            case int n when ((tank + 1) < n && n <= fast):
                Instantiate(enemyPrefabs[1], transform);
                break;

            case int n when ((fast + 1) < n):
                Instantiate(enemyPrefabs[2], transform);
                break;
        }
        }
}
