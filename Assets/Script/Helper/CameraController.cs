using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 60f;
    public float zoomSpeed = 100f;
    public Transform rotationPoint;

    private Vector3 originalDistance;

    private void Start()
    {
        originalDistance = transform.position - rotationPoint.position;
    }

    private void Update()
    {
        // 获取旋转和缩放输入
        float hInput = Input.GetAxis("Horizontal");  // AD键
        float vInput = Input.GetAxis("Vertical");    // WS键
        float zoomInput = Input.GetAxis("Zoom");     // QE键

        // 左右旋转
        transform.RotateAround(rotationPoint.position, Vector3.up, hInput * rotationSpeed * Time.deltaTime);

        // 上下旋转
        transform.RotateAround(rotationPoint.position, transform.right, -vInput * rotationSpeed * Time.deltaTime);

        // 调整相机距离
        originalDistance += zoomInput * zoomSpeed * Time.deltaTime * transform.forward;
        transform.position = rotationPoint.position + originalDistance;

        // 当鼠标中键被按下，移动旋转中心
        if (Input.GetMouseButtonDown(2))  // 鼠标中键
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                rotationPoint.position = hit.point;
            }
        }
    }
}