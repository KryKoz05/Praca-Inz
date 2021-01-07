using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IStack<Node>
{
    public bool walkable;
    public Vector3 worldPos;
    public int costA;
    public int costB;
    public int posX;
    public int posY;
    public Node root;
    int index;

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

   

    public int NodeCompare(Node toCompare)
    {
        int compare = costC.CompareTo(toCompare.costC);
        if (compare == 0)
        {
            compare = costB.CompareTo(toCompare.costB);
        }
        return -compare;
    }
    public int Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

    public int CompareTo(Node toCompare)
    {
        int compare = costC.CompareTo(toCompare.costC);
        if (compare == 0)
        {
            compare = costB.CompareTo(toCompare.costB);
        }
        return -compare;
    }
}
