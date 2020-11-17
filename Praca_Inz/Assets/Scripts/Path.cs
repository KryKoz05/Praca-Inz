using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    Grid grid;

    public Transform seekFor, seeker;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        Find(seeker.position, seekFor.position);
    }
    void Find(Vector3 start, Vector3 target)
    {
        Node startNode = grid.NodeFromWorldPoint(start);
        Node targetNode = grid.NodeFromWorldPoint(target);

        List<Node> setOpen = new List<Node>();
        HashSet<Node> setClose = new HashSet<Node>();
        setOpen.Add(startNode);

        while (setOpen.Count > 0)
        {
            Node currNode = setOpen[0];
            for (int i = 1; i < setOpen.Count; i++)
            {
                if ( setOpen[i].costA < currNode.costA || setOpen[i].costA == currNode.costA && setOpen[i].costB < currNode.costB)
                {
                    currNode = setOpen[i];
                }
            }

            setOpen.Remove(currNode);
            setClose.Add(currNode);

            if(currNode == targetNode)
            {
                ExtractPath(startNode, targetNode);
                return;
            }

            foreach (Node n in grid.neighbours(currNode))
            {
                if(!n.walkable || setClose.Contains(n))
                {
                    continue;
                }

                int newCost = currNode.costA + Distance(currNode, n);
                if (newCost < n.costA || !setOpen.Contains(n))
                {
                    n.costA = newCost;
                    n.costB = Distance(n, targetNode);
                    n.p = currNode;

                    if (!setOpen.Contains(n))
                    {
                        setOpen.Add(n);
                    }
                }

            }
        }
    }

    void ExtractPath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currNode = end;

        while (currNode != start)
        {
            path.Add(currNode);
            currNode = currNode.p;
        }
        path.Reverse();

        grid.path = path;
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
