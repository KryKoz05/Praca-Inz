using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorytm : MonoBehaviour
{
    Siatka siatka;
    ReqestManager pathRequest;
    

    private void Awake()
    {
        siatka = GetComponent<Siatka>();
        pathRequest = GetComponent<ReqestManager>();
    }


    public void Search (Vector3 start, Vector3 end)
    {
        StartCoroutine(Find(start, end));
    }

    IEnumerator Find(Vector3 start, Vector3 target)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSucces = false;

        Wezel startNode = siatka.NodeFromWorldPoint(start);
        Wezel targetNode = siatka.NodeFromWorldPoint(target);
        if (startNode.walkable && targetNode.walkable)
        {
            Stos<Wezel> setOpen = new Stos<Wezel>(siatka.GridSize);
            HashSet<Wezel> setClose = new HashSet<Wezel>();
            setOpen.Dodaj(startNode);

            while (setOpen.Licz > 0)
            {
                Wezel currNode = setOpen.Usun();
                

                
                setClose.Add(currNode);

                if (currNode == targetNode)
                {
                    pathSucces = true;
                    break;
                }

                foreach (Wezel n in siatka.Neighbours(currNode))
                {
                    if (!n.walkable || setClose.Contains(n))
                    {
                        continue;
                    }

                    int newCost = currNode.costA + Distance(currNode, n);
                    if (newCost < n.costA || !setOpen.Zawiera(n))
                    {
                        n.costA = newCost;
                        n.costB = Distance(n, targetNode);
                        n.poprzednik = currNode;

                        if (!setOpen.Zawiera(n))
                        {
                            setOpen.Dodaj(n);
                        }

                    }

                }
            }
        }

         
    
        yield return null;
        if (pathSucces)
        {
            waypoints = ExtractPath(startNode, targetNode);
        }
        pathRequest.Finished(waypoints, pathSucces);

    }

    Vector3[] ExtractPath(Wezel start, Wezel end)
    {
        List<Wezel> path = new List<Wezel>();
        Wezel currNode = end;

        while (currNode != start)
        {
            path.Add(currNode);
            currNode = currNode.poprzednik;
        }
        Vector3[] waypoints = Simp(path);
        Array.Reverse(waypoints);
        return waypoints;
       
    }

    Vector3[] Simp(List<Wezel> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 dirOld = Vector2.zero;

        for(int i = 1; i < path.Count; i++)
        {
            Vector2 dirNew = new Vector2(path[i - 1].posX - path[i].posX, path[i - 1].posY - path[i].posY);
            if(dirNew != dirOld)
            {
                waypoints.Add(path[i-1].worldPos);
            }
            dirOld = dirNew;
        }
        return waypoints.ToArray();
    }

    int Distance(Wezel A, Wezel B)
    {
        int dsX = Mathf.Abs(A.posX - B.posX);
        int dsY = Mathf.Abs(A.posY - B.posY);

        if (dsX > dsY)
            return 14 * dsY + 10 * (dsX - dsY);
        return 14 * dsX + 10 * (dsY - dsX);
    }
}
