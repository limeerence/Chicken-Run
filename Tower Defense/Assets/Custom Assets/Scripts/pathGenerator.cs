using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private GameObject towerLocationPrefab;
    [SerializeField] private Canvas mapCanvas;
    [SerializeField] private int size = 20;
    private int[,] grid;
    public static List<GameObject> path;
    private int yVal = 1;

    private void Awake()
    {
        grid = new int[size, size];
        path = new List<GameObject>();
        createGrid();
    }

    private void createGrid()
    {
        int x = 0, z = UnityEngine.Random.Range(0, size), nextMove, prevMove = -1;

        path.Add(Instantiate(waypointPrefab, new Vector3(x, yVal, z), Quaternion.identity, transform));
        path[path.Count - 1].name = (path.Count - 1).ToString();
        grid[x, z] = 1;
        while(x < size - 1)
        {
            nextMove = Random.Range(0, 3);
            while((nextMove == 0 && prevMove == 1) || (nextMove == 1 && prevMove == 0))
                nextMove = Random.Range(0, 3);
            prevMove = nextMove;
            switch (nextMove)
            {
                case 0:
                    if (z < size - 1)
                        z++;
                    else
                    {
                        x++;
                        prevMove = 2;
                    }
                    break;
                case 1:
                    if (z > 0)
                        z--;
                    else
                    {
                        x++;
                        prevMove = 2;
                    }
                    break;
                case 2:
                    x++;
                    break;
            }
            fillGridWithPath(x, z);
        }
        createTowerLocations();
    }

    private void fillGridWithPath(int x, int z)
    {
        path.Add(Instantiate(waypointPrefab, new Vector3(x, yVal, z), Quaternion.identity, transform));
        path[path.Count - 1].name = (path.Count - 1).ToString();
        grid[x, z] = 1;
    }

    private void createTowerLocations()
    {
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                if (grid[x, z] == 0 && canPlaceTower(x,z))
                {
                    grid[x, z] = 2;
                    Instantiate(towerLocationPrefab, new Vector3(x, 0, z), Quaternion.Euler(90,0,0), mapCanvas.transform);
                }
                
            }
        }
    }

    private bool canPlaceTower(int x, int z)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int a = x + i;
                int b = z + j;
                if (a >=0 && b >= 0 && a < size && b < size && grid[a,b] == 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
