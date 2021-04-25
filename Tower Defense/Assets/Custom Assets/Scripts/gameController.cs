using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public int round = 1;
    public int health = 100;
    public int coins = 5;

    [SerializeField] private Text roundText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text coinsText;

    [SerializeField] public Image healthImage;
    [SerializeField] public Sprite[] healthSprites;

    [SerializeField] private Image gameOverImage;
    [SerializeField] private Image controlsImage;
    [SerializeField] private Image nextRoundImage;

    private void Start()
    {
        if (round == 1)
        {
            StartCoroutine("MoveControlsPopup");
        }

        roundText.text = "" + round;
        healthText.text = "" + health;
        coinsText.text = "" + coins;
    }

    private void Update()
    {
        
    }

    public void updateHealth(int addHealth)
    {
        health += addHealth;
        healthText.text = "" + health;
        if (health <= 0)
        {
            GameOver();
        }
        if (health < 25)
        {
            healthImage.sprite = healthSprites[1];
        }
        else if (health < 40)
        {
            healthImage.sprite = healthSprites[2];
        }
        else if (health < 56)
        {
            healthImage.sprite = healthSprites[3];
        }
        else if (health < 76)
        {
            healthImage.sprite = healthSprites[4];
        }
        else if (health < 100)
        {
            healthImage.sprite = healthSprites[5];
        }
        else if (health == 100)
        {
            healthImage.sprite = healthSprites[6];
        }

    }

    public void GameOver()
    {
        healthImage.sprite = healthSprites[0];
        gameOverImage.gameObject.SetActive(true);
        Debug.Log("GAME OVER!!");
        Invoke("returnToMenu", 3);
    }

    private void returnToMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator MoveControlsPopup()
    {
        controlsImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        controlsImage.gameObject.SetActive(false);
    }
}
