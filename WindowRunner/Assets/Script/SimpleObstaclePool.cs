using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool : MonoBehaviour
{
    public static SimpleObjectPool Instance;

    private Dictionary<string, Queue<GameObject>> poolDict = new();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject GetFromPool(GameObject prefab)
    {
        string key = prefab.name;

        if (poolDict.ContainsKey(key) && poolDict[key].Count > 0)
        {
            GameObject obj = poolDict[key].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject newObj = Instantiate(prefab);
        newObj.name = key;
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        string key = obj.name.Replace("(Clone)", "").Trim();

        if (!poolDict.ContainsKey(key))
            poolDict[key] = new Queue<GameObject>();

        poolDict[key].Enqueue(obj);
        obj.transform.SetParent(this.transform, false);
    }
}
