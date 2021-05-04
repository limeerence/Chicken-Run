using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class gameController : MonoBehaviour
{
    public int round = 1;
    public int health = 100;
    public int coins = 5;
    public int wavesRemaining = 1;
    public int thisWave = 0;
    public int enemiesRemaining = 1;

    [SerializeField] private Text roundText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text thisWaveText;
    [SerializeField] private Text enemiesText;

    [SerializeField] public Image healthImage;
    [SerializeField] public Sprite[] healthSprites;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Image controlsImage;
    [SerializeField] private Image nextRoundImage;

    [SerializeField] private Image noCoinsImage;
    private bool coinsActive = false;
    private bool isNextRound = false;
    private GameObject[] allEnemies;

    private void Start()
    {
        if (round == 1)
        {
            //StartCoroutine("MoveControlsPopup");
        }

        roundText.text = "Round " + round.ToString();
        healthText.text = health.ToString();
        coinsText.text = coins.ToString();
        wavesRemaining = round;
        thisWaveText.text = "Waves: " + thisWave.ToString() + "/" + round.ToString();
    }

    private void Update()
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesRemaining = allEnemies.Length;
        enemiesText.text = "Enemies: " + enemiesRemaining.ToString();
        coinsText.text = coins.ToString();
        thisWaveText.text = "Waves: " + thisWave.ToString() + "/" + round.ToString();
        if (wavesRemaining == 0 && !isNextRound && enemiesRemaining == 0)
        {
            isNextRound = true;
            StartCoroutine(nextRound());
        }
    }

    IEnumerator nextRound()
    {
        Debug.Log("End round " + round);
        round++;
        nextRoundImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        nextRoundImage.gameObject.SetActive(false);
        wavesRemaining = round;
        thisWave = 0;
        roundText.text = "Round " + round.ToString();
        isNextRound = false;
    }

    public void updateHealth(int addHealth)
    {
        health += addHealth;
        healthText.text = health.ToString();

        switch (health)
        {
            case int n when (n <= 0):
                GameOver();
                break;

            case int n when (n < 26):
                healthImage.sprite = healthSprites[1];
                break;

            case int n when (n < 40):
                healthImage.sprite = healthSprites[2];
                break;

            case int n when (n < 56):
                healthImage.sprite = healthSprites[3];
                break;

            case int n when (n < 76):
                healthImage.sprite = healthSprites[4];
                break;

            case int n when (n < 100):
                healthImage.sprite = healthSprites[5];
                break;

            case int n when (n >= 100):
                healthImage.sprite = healthSprites[6];
                break;
        }

    }

    public void GameOver()
    {
        healthImage.sprite = healthSprites[0];
        gameOverPanel.gameObject.SetActive(true);
        Debug.Log("GAME OVER!!");
    }

    IEnumerator MoveControlsPopup()
    {
        controlsImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        controlsImage.gameObject.SetActive(false);
    }

    public IEnumerator noCoins()
    {
        if (!coinsActive)
        {
            coinsActive = true;
            noCoinsImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            noCoinsImage.gameObject.SetActive(false);
            coinsActive = false;
        } else
        {
            yield return new WaitForSeconds(1);
            noCoinsImage.gameObject.SetActive(false);
            coinsActive = false;
        }
    }
}
