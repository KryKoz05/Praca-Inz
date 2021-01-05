using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.SceneManagement;

public class RequestManager : MonoBehaviour
{
    Queue<Result> results = new Queue<Result>();
    Algorithm findPath;
    

    Queue<PathRequest> pathRequests = new Queue<PathRequest>();
    Thread thread;
    bool gameRunning = true;

    static RequestManager instance;

    private void Awake()
    {
        
        instance = this;
        findPath = GetComponent<Algorithm>();

        ThreadStart ts = delegate
        {
            PathRequestsUpdate();
        };
        thread = new Thread(ts);
        thread.Start();

        Application.quitting += Quitting;
        
    }

 

    private void Update()
    {
        if (results.Count > 0)
        {
            int queueSize = results.Count;
            lock (results) { };
            for (int i = 0; i < queueSize; i++)
            {
                Result result = results.Dequeue();
                result.retrive(result.path, result.succes);
            }
        }
    }

    private void Quitting()
    {
        gameRunning = false;
    }

    public void PathRequestsUpdate()
    {
        PathRequest pathRequest;

        
        while (gameRunning)
        {
            if (pathRequests.Count > 0)
            {
                lock (pathRequests)
                {
                    pathRequest = pathRequests.Dequeue();
                }
                findPath.Find(pathRequest, Finished);
            }
        }
    }

    public static void Request(PathRequest request)
    {
        lock (instance.pathRequests)
        {
            instance.pathRequests.Enqueue(request);
        }


    }

    public void Finished(Result result)
    {

        lock (results)
        {
            results.Enqueue(result);
        }
    }


}


public struct PathRequest
{
    public Vector3 start;
    public Vector3 end;
    public Action<Vector3[], bool> retrive;

    public PathRequest(Vector3 dstart, Vector3 dend, Action<Vector3[], bool> dretrive)
    {
        start = dstart;
        end = dend;
        retrive = dretrive;
    }
}

public struct Result
{
    public Vector3[] path;
    public bool succes;
    public Action<Vector3[], bool> retrive;

    public Result(Vector3[] path, bool succes, Action<Vector3[], bool> retrive)
    {
        this.path = path;
        this.succes = succes;
        this.retrive = retrive;
    }
}