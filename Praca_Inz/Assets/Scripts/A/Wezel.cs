using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wezel : IStosik<Wezel>
{
    public bool walkable;
    public Vector3 worldPos;
    public int costA;
    public int costB;
    public int posX;
    public int posY;
    public Wezel poprzednik;
    int indeks;

    public Wezel(bool dWalkable, Vector3 dWorldPos, int dPosX, int dPosY)
    {
        walkable = dWalkable;
        worldPos = dWorldPos;
        posX = dPosX;
        posY = dPosY;
    }


    public int CostC
    {
        get
        {
            return costA + costB;
        }
    }

   

    public int NodeCompare(Wezel toCompare)
    {
        int compare = CostC.CompareTo(toCompare.CostC);
        if (compare == 0)
        {
            compare = costB.CompareTo(toCompare.costB);
        }
        return -compare;
    }
    public int Indeks
    {
        get
        {
            return indeks;
        }

        set
        {
            indeks = value;
        }
    }

    public int CompareTo(Wezel doPorownania)
    {
        int porownaj = CostC.CompareTo(doPorownania.CostC);
        if (porownaj == 0)
        {
            porownaj = costB.CompareTo(doPorownania.costB);
        }
        return -porownaj;
    }
}
