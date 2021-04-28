using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPrefabs;

    private bool hasTower = false;

    public void addTower()
    {
        if (!hasTower)
        {
            int s = Random.Range(0, towerPrefabs.Length);
            Instantiate(towerPrefabs[s], transform.position, Quaternion.identity);
            hasTower = true;
        }
    }
}
