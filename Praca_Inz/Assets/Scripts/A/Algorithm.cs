using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    Grid grid;
    RequestManager pathRequest;
    

    void Awake()
    {
        grid = GetComponent<Grid>();
        
    }


   

    public void Find(PathRequest request, Action<Result> retrive)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSucces = false;

        Node startNode = grid.NodeFromWorldPoint(request.start);
        Node endNode = grid.NodeFromWorldPoint(request.end);
        if (startNode.walkable && endNode.walkable)
        {
            Stack<Node> setOpen = new Stack<Node>(grid.GridSize);
            HashSet<Node> setClose = new HashSet<Node>();
            setOpen.Add(startNode);

            while (setOpen.Count > 0)
            {
                Node currNode = setOpen.Delete();
                

                
                setClose.Add(currNode);

                if (currNode == endNode)
                {
                    pathSucces = true;
                    break;
                }

                foreach (Node n in grid.Neighbours(currNode))
                {
                    if (!n.walkable || setClose.Contains(n))
                    {
                        continue;
                    }

                    int newCost = currNode.costA + Distance(currNode, n);
                    if (newCost < n.costA || !setOpen.Includes(n))
                    {
                        n.costA = newCost;
                        n.costB = Distance(n, endNode);
                        n.root = currNode;

                        if (!setOpen.Includes(n))
                        {
                            setOpen.Add(n);
                        }
                        else
                        {
                            setOpen.Update(n);
                        }

                    }

                }
            }
        }

         
    
       
        if (pathSucces)
        {
            waypoints = ExtractPath(startNode, endNode);
            pathSucces = waypoints.Length > 0;
        }
        retrive(new Result(waypoints, pathSucces, request.retrive));

    }

    Vector3[] ExtractPath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currNode = end;

        while (currNode != start)
        {
            path.Add(currNode);
            currNode = currNode.root;
        }
        Vector3[] waypoints = Simp(path);
        Array.Reverse(waypoints);
        return waypoints;
       
    }

    Vector3[] Simp(List<Node> path)
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

    int Distance(Node A, Node B)
    {
        int dsX = Mathf.Abs(A.posX - B.posX);
        int dsY = Mathf.Abs(A.posY - B.posY);

        if (dsX > dsY)
            return 14 * dsY + 10 * (dsX - dsY);
        return 14 * dsX + 10 * (dsY - dsX);
    }
}
