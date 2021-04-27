using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private int size = 20;
    private int[,] grid;
    public static List<GameObject> path;

    private void Awake()
    {
        grid = new int[size, size];
        path = new List<GameObject>();
        createGrid();
    }

    private void createGrid()
    {
        int x = 0, z = UnityEngine.Random.Range(0, size), nextMove, prevMove = -1;

        path.Add(Instantiate(waypointPrefab, new Vector3(x, 0, z), Quaternion.identity, transform));
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
    }

    private void fillGridWithPath(int x, int z)
    {
        path.Add(Instantiate(waypointPrefab, new Vector3(x, 0, z), Quaternion.identity, transform));
        path[path.Count - 1].name = (path.Count - 1).ToString();
        grid[x, z] = 1;
    }
}
