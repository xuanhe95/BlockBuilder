using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    private Transform cameraTransform;
    private Vector3 origin = new Vector3(5,5,5);
    private Generator gen;

    void Start()
    {
        gen = GameObject.FindObjectOfType<Generator>();
        origin = new Vector3((float)gen.width, (float)gen.height, (float)gen.length) / 2;
        cameraTransform = transform;
        cameraTransform.LookAt(origin);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // AD control
        float vertical = Input.GetAxis("Vertical"); // WS control

        if (horizontal != 0 || vertical != 0)
        {
            RotateCamera(horizontal, vertical);
        }
    }

    private void RotateCamera(float horizontal, float vertical)
    {
        // Rotate around Y axis (left and right)
        cameraTransform.RotateAround(origin, Vector3.up, -horizontal * rotationSpeed * Time.deltaTime);

        // Rotate around X axis (up and down)
        cameraTransform.RotateAround(origin, cameraTransform.right, vertical * rotationSpeed * Time.deltaTime);

        // Maintain camera's orientation towards the origin
        cameraTransform.LookAt(origin);
    }
}