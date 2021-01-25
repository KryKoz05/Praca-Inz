using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDic;


    void Start()
    {
        poolDic = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDic.Add(pool.tag, objectPool);
        }
    }

    public GameObject PoolSpawner( string tag, Vector3 pos, Quaternion rotation)
    {

        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogWarning("This poool doesnt exist");
            return null;
        }
        GameObject objToSpawn = poolDic[tag].Dequeue();

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos;
        objToSpawn.transform.rotation = rotation;

        IPooledObj pooledObj = objToSpawn.GetComponent<IPooledObj>();

        if( pooledObj != null)
        {
            pooledObj.SpawnObject();
        }

        poolDic[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }

  
}
