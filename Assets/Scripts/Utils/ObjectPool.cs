using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    // Pools for objects
    private Queue<GameObject> pool = new Queue<GameObject>();

    public ObjectPool(GameObject prefab, int instances, GameObject parent)
    {
        // Put information about our pools to Dictionary
        for(int i = 0; i < instances; i++)
        {
            GameObject go = UnityEngine.Object.Instantiate(prefab);
            go.transform.parent = parent.transform;
            pool.Enqueue(go);
            go.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        // If there are free object in pool return it
        if(pool.Count > 0)
        {
            return pool.Dequeue();
        }

        return null;
    }

    public void ReturnObject(GameObject poolObject)
    {
        // Return object to pool
        pool.Enqueue(poolObject);
    }
}