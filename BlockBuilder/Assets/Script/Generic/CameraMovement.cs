using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float rotationSpeed = 1.0f;

    private void Update()
    {
        // 获取键盘输入
        float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        // 计算摄像机的旋转角度
        float angle = rotationSpeed * Time.deltaTime;
        Vector3 rotation = new Vector3(0, -horizontal * angle, 0);

        // 围绕原点旋转摄像机
        transform.RotateAround(new Vector3(5,5,5), rotation, angle);
    }
}