using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class towerSelector : MonoBehaviour
{
    public GameObject towerLocation;
    [SerializeField] private gameController controller;
    private int magicCost = 10;
    private int fireCost = 3;
    private int iceCost = 5;

    private void Awake()
    {
        if (!controller)
        {
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        }
    }

    public void MagicTower()
    {
        if (controller.coins >= magicCost)
        {
            controller.coins -= magicCost;
            towerLocation.GetComponent<towerGenerator>().towerSelected = 0;
            towerLocation.GetComponent<towerGenerator>().addTower();
            towerLocation = null;
        } else
        {
            StartCoroutine(controller.noCoins());
        }

    }

    public void FireTower()
    {
        if (controller.coins >= fireCost)
        {
            controller.coins -= fireCost;
            towerLocation.GetComponent<towerGenerator>().towerSelected = 1;
            towerLocation.GetComponent<towerGenerator>().addTower();
            towerLocation = null;
        } else
        {
            StartCoroutine(controller.noCoins());
        }
    }

    public void IceTower()
    {
        if (controller.coins >= iceCost)
        {
            controller.coins -= iceCost;
            towerLocation.GetComponent<towerGenerator>().towerSelected = 2;
            towerLocation.GetComponent<towerGenerator>().addTower();
            towerLocation = null;
        } else
        {
            StartCoroutine(controller.noCoins());
        }
    }

    public void closeMenu()
    {
        gameObject.SetActive(false);
    }

}
