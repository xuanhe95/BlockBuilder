using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPoints : MonoBehaviour
{
    public int numberOfPoints = 100; // 生成的点的数量
    public float minX = -5f; // 点的X轴最小值
    public float maxX = 5f; // 点的X轴最大值
    public float minY = -5f; // 点的Y轴最小值
    public float maxY = 5f; // 点的Y轴最大值

    public GameObject pointPrefab; // 用于实例化的点预制件

    void Start()
    {
        GeneratePoints();
    }

    void GeneratePoints()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            // 生成随机点的坐标
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);

            // 实例化点预制件并设置位置
            GameObject point = Instantiate(pointPrefab, new Vector3(x, 0, y), Quaternion.identity);
            point.name = "Point " + (i + 1);
        }
    }
}