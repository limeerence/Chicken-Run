using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class towerGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private GameObject towerSelectionPanel;
    [SerializeField] private GameObject towerUpgradePanel;

    public GameObject myTower;
    public int towerSelected = 0;
    private bool hasTower = false;
    private bool maxedTower = false;
    public int upgradeCost;
    public int currLevel;

    private void Awake()
    {
        towerSelectionPanel = GameObject.Find("Tower Select Panel");
        
        towerUpgradePanel = GameObject.Find("Tower Upgrade Panel");   
    }

    private void Start()
    {
        towerSelectionPanel.SetActive(false);
        towerUpgradePanel.SetActive(false);
    }

    public void editTower()
    {
        if (!hasTower)
        {
            towerSelectionPanel.GetComponent<towerSelector>().towerLocation = gameObject;
            towerUpgradePanel.gameObject.SetActive(false);
            towerSelectionPanel.SetActive(true); 
        } else if (!maxedTower)
        {
            towerSelectionPanel.SetActive(false);
            towerUpgradePanel.GetComponent<towerUpgrader>().towerLocation = gameObject;
            towerUpgradePanel.GetComponent<towerUpgrader>().activateMenu();
        }
    }

    public void addTower()
    {
        towerSelectionPanel.SetActive(false);
        myTower = Instantiate(towerPrefabs[towerSelected], transform.position, Quaternion.identity);
        if (towerSelected == 2)
        {
            myTower.GetComponent<AudioSource>().Play();
        }
        upgradeCost = myTower.GetComponent<Tower>().upgradeCost[1];
        currLevel = myTower.GetComponent<Tower>().upgradeLevel;
        hasTower = true;
    }

    public void upgradeTower()
    {
        towerUpgradePanel.SetActive(false);
        currLevel = myTower.GetComponent<Tower>().upgradeLevel;
        int nextLevel = currLevel + 1;
        if (nextLevel < 3)
        {
            myTower.GetComponent<Tower>().towerUpgrades[currLevel].SetActive(false);
            myTower.GetComponent<Tower>().towerUpgrades[nextLevel].SetActive(true);
            myTower.GetComponent<Tower>().upgradeLevel = nextLevel;
            currLevel = nextLevel;
            upgradeCost = myTower.GetComponent<Tower>().upgradeCost[nextLevel + 1];
            myTower.GetComponent<Tower>().towerDamage += myTower.GetComponent<Tower>().upgradeDPS;
            myTower.GetComponent<Tower>().shootRate += myTower.GetComponent<Tower>().upgradeRate;
        }
        else if (nextLevel == 3)
        {
            myTower.GetComponent<Tower>().towerUpgrades[currLevel].SetActive(false);
            myTower.GetComponent<Tower>().towerUpgrades[nextLevel].SetActive(true);
            myTower.GetComponent<Tower>().upgradeLevel = nextLevel;
            currLevel = nextLevel;
            myTower.GetComponent<Tower>().towerDamage += myTower.GetComponent<Tower>().upgradeDPS;
            myTower.GetComponent<Tower>().shootRate += myTower.GetComponent<Tower>().upgradeRate;

            //maxed effect
            myTower.GetComponent<Tower>().particles.SetActive(true);
            maxedTower = true;
        }


    }
}
