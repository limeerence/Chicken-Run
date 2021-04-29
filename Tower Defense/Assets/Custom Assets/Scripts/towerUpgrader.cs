using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class towerUpgrader : MonoBehaviour
{
    public GameObject towerLocation;
    private int level = 0;
    
    [SerializeField] private Text costText;
    [SerializeField] private Text levelText;
    [SerializeField] private gameController controller;

    private void Awake()
    {
        if (!controller)
        {
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        }
    }

    public void upgradeTower()
    {
        if (towerLocation.GetComponent<towerGenerator>().upgradeCost <= controller.coins)
        {
            controller.coins -= towerLocation.GetComponent<towerGenerator>().upgradeCost;
            towerLocation.GetComponent<towerGenerator>().upgradeTower();
        } else
        {
            StartCoroutine(controller.noCoins());
        }
        
    }

    public void closeMenu()
    {
        gameObject.SetActive(false);
    }

    public void activateMenu()
    {
        //level = towerLocation.GetComponent<towerGenerator>().currLevel + 1;
        //levelText.text = "[Lv. " + level.ToString() + "]";
        costText.text = towerLocation.GetComponent<towerGenerator>().upgradeCost.ToString();
        gameObject.SetActive(true);
    }

}
