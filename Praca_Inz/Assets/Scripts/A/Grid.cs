using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Vector2 worldSize;
    public float r;
    public LayerMask obst;
    Node[,] grid;

    float  diameter;
    int gridSizeX, gridSizeY;

    

    public int GridSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }


    void Awake()
    {
        diameter = r * 2;
        gridSizeX = Mathf.RoundToInt(worldSize.x /  diameter);
        gridSizeY = Mathf.RoundToInt(worldSize.y /  diameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * worldSize.x/2 - Vector3.up * worldSize.y/2;

        for ( int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (i *  diameter + r) + Vector3.up * (j *  diameter + r);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, r, obst));
                grid[i, j] = new Node(walkable, worldPoint, i , j);
            }
        }
    }

    public Node  NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x / worldSize.x + 0.5f);
        float percentY = (worldPosition.y / worldSize.y + 0.5f);

        percentX = (worldPosition.x + worldSize.x / 2) / worldSize.x;
        percentY = (worldPosition.y + worldSize.y / 2) / worldSize.y;

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> Neighbours(Node n)
    {
        List<Node> n1 = new List<Node>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                int chX = n.posX + i;
                int chY = n.posY + j;

                if (chX >= 0 && chX < gridSizeX && chY >= 0 && chY < gridSizeY)
                {
                    n1.Add(grid[chX, chY]);
                }
            }
        }

        return n1;
    }



}
