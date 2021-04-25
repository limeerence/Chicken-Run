using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private int size = 20;
    private int[,] grid;

    private void Start()
    {
        grid = new int[size, size];
        createGrid();
    }

    private void createGrid()
    {
        
    }
}
