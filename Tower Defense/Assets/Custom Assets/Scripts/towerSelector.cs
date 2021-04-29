using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class towerSelector : MonoBehaviour
{
    public GameObject towerLocation;
    [SerializeField] private gameController controller;

    private void Awake()
    {
        if (!controller)
        {
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        }
    }

    public void MagicTower()
    {
        if (controller.coins >= 10)
        {
            controller.coins -= 10;
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
        if (controller.coins >= 2)
        {
            controller.coins -= 2;
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
        if (controller.coins >= 5)
        {
            controller.coins -= 5;
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
