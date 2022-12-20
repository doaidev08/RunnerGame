using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int poolAmount = 15;

    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for(int i=0;i<poolAmount; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.transform.rotation = objectToPool.transform.rotation;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPoolObject()
    {
        for(int i = 0; i< poolAmount; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i]; 
            }
            
        }
        return null; 
    }
}
