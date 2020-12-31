using System.Collections.Generic;
using UnityEngine;
using System;

public class ReqestManager : MonoBehaviour
{

    Queue<PathRequest> pathQueue = new Queue<PathRequest>();
    PathRequest currPath;
    Algorytm findPath;
    bool ProcessPath;
    static ReqestManager instance;

    private void Awake()
    {
        instance = this;
        findPath = GetComponent<Algorytm>();
    }


    public static void Request( Vector3 start, Vector3 end, Action<Vector3[], bool> retrive)
    {
        PathRequest newReq = new PathRequest(start, end, retrive);
        instance.pathQueue.Enqueue(newReq);
        instance.ProcessNext();
    }

    void ProcessNext()
    {
        if (!ProcessPath && pathQueue.Count > 0)
        {
            currPath = pathQueue.Dequeue();
            ProcessPath = true;
            findPath.Search(currPath.start, currPath.end);
        }
    }

    public void Finished(Vector3[] path, bool success)
    {
        currPath.retrive(path, success);
        ProcessPath = false;
        if(this.GetComponent<Unit>()!=null)
        ProcessNext();
    }

    struct PathRequest
    {
        public Vector3 start;
        public Vector3 end;
        public Action<Vector3[], bool> retrive;

        public  PathRequest(Vector3 dstart, Vector3 dend, Action<Vector3[], bool> dretrive)
        {
            start = dstart;
            end = dend;
            retrive = dretrive;
        }
    }
}
