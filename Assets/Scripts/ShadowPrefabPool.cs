using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPrefabPool : MonoBehaviour
{
    public static ShadowPrefabPool instance;

    public GameObject shadowPrefab;

    public int shadowCount;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    void Awake()
    {
        instance = this;

        FillPool();//初始化
    }

    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)//做指定数量的不可见的shadow
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);

            //放回到prefab池里的方法
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);

        availableObjects.Enqueue(gameObject);//加入队列尾部
    }

    public GameObject GetFormPool()
    {
        if (availableObjects.Count == 0)
        {
            FillPool();
        }

        var outShadow = availableObjects.Dequeue();//从队列头出来一个

        outShadow.SetActive(true);

        return outShadow;
    }
}
