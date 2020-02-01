using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public int amountToPool;
        public GameObject objectToPool;
        public bool shouldExpand;
    }

    public static PoolManager SharedInstance;

    private List<GameObject> pooledObjects;
    public ObjectPoolItem[] itemsToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                var poolItem = (GameObject)Instantiate(item.objectToPool);
                poolItem.transform.parent=transform;
                poolItem.SetActive(false);
                pooledObjects.Add(poolItem);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    var poolItem = (GameObject)Instantiate(item.objectToPool);
                    poolItem.transform.parent=transform;
                    poolItem.SetActive(false);
                    pooledObjects.Add(poolItem);
                    return poolItem;
                }
            }
        }
        return null;
    }

    public GameObject GetPooledObjectWithoutInstantiate(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    //public GameObject GetRandomPooledObject(string tag)
    //{
    //    ListUtility.Shuffle(pooledObjects);
    //    return GetPooledObject(tag);
    //}

    // private void ResetObject(GameObject go){
    //     go.transform.position = go.originalPosition.position;
    //     go.transform.rotation = go.originalPosition.rotation;
    // }
}
