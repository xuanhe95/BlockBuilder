using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestObjects : MonoBehaviour
{
    public List<GameObject> objectsToCheck; // 要检查的物体列表

    void Start()
    {
        FindClosestObjects();
    }

    void FindClosestObjects()
    {
        if (objectsToCheck.Count < 2)
        {
            Debug.LogError("至少需要两个物体才能计算最接近的物体");
            return;
        }

        GameObject closestObject1 = null;
        GameObject closestObject2 = null;
        float closestDistance = Mathf.Infinity;

        // 迭代所有的物体
        for (int i = 0; i < objectsToCheck.Count; i++)
        {
            for (int j = i + 1; j < objectsToCheck.Count; j++)
            {
                // 计算两个物体之间的距离
                float distance = Vector3.Distance(objectsToCheck[i].transform.position, objectsToCheck[j].transform.position);

                // 如果距离比当前最近的距离更短，则更新最近的物体
                if (distance < closestDistance)
                {
                    closestObject1 = objectsToCheck[i];
                    closestObject2 = objectsToCheck[j];
                    closestDistance = distance;
                }
            }
        }

        Debug.Log("最接近的两个物体是 " + closestObject1.name + " 和 " + closestObject2.name + "，距离为 " + closestDistance);
    }
}