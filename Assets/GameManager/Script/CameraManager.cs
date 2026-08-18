using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Pan")]
    public float panSpeed = 1f;

    [Header("Zoom")]
    public float zoomSpeed = 5f;
    public float minZoom = 3f;
    public float maxZoom = 20f;

    [Header("Map Limit")]
    public float mapMinX = -50f;
    public float mapMaxX = 50f;
    public float mapMinY = -50f;
    public float mapMaxY = 50f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        PanCamera();
        ZoomCamera();
        ClampCamera();
    }

    void PanCamera()
    {
        // Giữ chuột giữa
        if (Input.GetMouseButton(2))
        {
            float moveX = -Input.GetAxis("Mouse X") * panSpeed;
            float moveY = -Input.GetAxis("Mouse Y") * panSpeed;

            transform.position += new Vector3(moveX, moveY, 0);
        }
    }

    void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed;

            cam.orthographicSize = Mathf.Clamp(
                cam.orthographicSize,
                minZoom,
                maxZoom
            );
        }
    }

    void ClampCamera()
    {
        // Chiều cao camera
        float cameraHeight = cam.orthographicSize;

        // Chiều rộng camera dựa trên aspect ratio
        float cameraWidth = cameraHeight * cam.aspect;

        float minX = mapMinX + cameraWidth;
        float maxX = mapMaxX - cameraWidth;

        float minY = mapMinY + cameraHeight;
        float maxY = mapMaxY - cameraHeight;

        float clampedX = Mathf.Clamp(
            transform.position.x,
            minX,
            maxX
        );

        float clampedY = Mathf.Clamp(
            transform.position.y,
            minY,
            maxY
        );

        transform.position = new Vector3(
            clampedX,
            clampedY,
            transform.position.z
        );
    }
}
