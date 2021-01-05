using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{


    public Transform end;
    [SerializeField]public float speed = 1;
    Vector3[] path;
    int endID;
    public bool alive = true;
    

    void FixedUpdate ()
    {


        RequestManager.Request(new PathRequest(transform.position, end.position, PathFounded));
        
    }

   

    public void PathFounded(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && alive)
        {
            path = newPath;
            endID = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 goTo = path[0];
        while (true)
        {
            if (transform.position == goTo)
            {
                endID++;
                if (endID >= path.Length)
                {
                    endID = 0;
                    path = new Vector3[0];
                    yield break;
                }
                goTo = path[endID];
            }

            transform.position = Vector3.MoveTowards(transform.position, goTo, speed * Time.deltaTime);
            yield return null;

        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = endID; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
               

                if (i == endID)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    
}
