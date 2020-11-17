using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPos;
    public int costA;
    public int costB;
    public int posX;
    public int posY;
    public Node p;

    public Node(bool dWalkable, Vector3 dWorldPos, int dPosX, int dPosY)
    {
        walkable = dWalkable;
        worldPos = dWorldPos;
        posX = dPosX;
        posY = dPosY;
    }

    public int costC
    {
        get
        {
            return costA + costB;
        }
    }
}
