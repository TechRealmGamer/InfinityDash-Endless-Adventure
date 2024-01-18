using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Object Pool")]
    [SerializeField] private int poolSize;
    [SerializeField] private GameObject[] prefabs;

    private List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        // Instantiate pool
        for (int i = 0, j = 0; j < poolSize; i++, j++)
        {
            if (i >= prefabs.Length)
                i = 0;

            GameObject obj = Instantiate(prefabs[i], transform.position, Quaternion.identity, transform);
            AddToPool(obj);
        }
    }

    // Add object to pool
    private void AddToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }

    // Get object from pool
    public GameObject GetFromPool()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            // If object is not active in hierarchy, return it
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        // If pool is empty, instantiate new object, add it to pool and return it
        poolSize++;
        GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position, Quaternion.identity, transform);
        pool.Add(obj);
        return obj;
    }
}
