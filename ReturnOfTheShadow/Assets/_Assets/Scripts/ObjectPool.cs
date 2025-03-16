using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "ObjectPool", menuName = "ObjectPool")]
public class ObjectPool : SerializedScriptableObject
{
    public Queue<GameObject> objectPool = new Queue<GameObject>();
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;

    public void LoadObjectPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab);
            return obj;
        }
    }
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
