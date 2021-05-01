using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int upgradeLevel = 0;
    public int upgradeDPS = 1;
    public int[] upgradeCost = new int[] { 5, 10, 15, 20 };
    public int towerDamage = 1;
    public float shootRate = 5f;
    public float upgradeRate = 0f;
    public float towerRange = 5f;
    public GameObject particles;
    public GameObject projectile;
    public GameObject towerHead;
    public GameObject[] towerUpgrades;
    public string towerType = "None";
}
