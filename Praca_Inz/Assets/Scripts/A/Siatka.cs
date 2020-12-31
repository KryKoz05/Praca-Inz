using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siatka : MonoBehaviour
{
    public Vector2 rozmiarSwiata;
    public float promien;
    public LayerMask przeszkoda;
    Wezel[,] siatka;

    float nodeDiameter;
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
        nodeDiameter = promien * 2;
        gridSizeX = Mathf.RoundToInt(rozmiarSwiata.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(rozmiarSwiata.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        siatka = new Wezel[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * rozmiarSwiata.x/2 - Vector3.up * rozmiarSwiata.y/2;

        for ( int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + promien) + Vector3.up * (y * nodeDiameter + promien);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, promien, przeszkoda));
                siatka[x, y] = new Wezel(walkable, worldPoint, x , y);
            }
        }
    }

    public Wezel NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x / rozmiarSwiata.x + 0.5f);
        float percentY = (worldPosition.y / rozmiarSwiata.y + 0.5f);

        percentX = (worldPosition.x + rozmiarSwiata.x / 2) / rozmiarSwiata.x;
        percentY = (worldPosition.y + rozmiarSwiata.y / 2) / rozmiarSwiata.y;

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return siatka[x, y];
    }

    public List<Wezel> Neighbours(Wezel n)
    {
        List<Wezel> n1 = new List<Wezel>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int chX = n.posX + x;
                int chY = n.posY + y;

                if (chX >= 0 && chX < gridSizeX && chY >= 0 && chY < gridSizeY)
                {
                    n1.Add(siatka[chX, chY]);
                }
            }
        }

        return n1;
    }



    void OnDrawGizmos()
        {
        Gizmos.DrawWireCube(transform.position, new Vector3(rozmiarSwiata.x, rozmiarSwiata.y, 1));
           
        if (siatka != null)
        {
       
            foreach (Wezel n in siatka)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;

                
                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
            }
        }
        }

}
