using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    [Header("CameraLimit")]
    public bool isLimitedCamPosition = true;
    public float cameraMinX = 0f;
    public float cameraMaxX = 1f;
    public bool ClampY = false;
    public float cameraMinY = 0f;
    public float cameraMaxY = 0f;
    public float cameraMinZ = 0f;
    public float cameraMaxZ = 1f;
    [Header("Other")]
    public float zoomFactor = 3f;
    public float ZoomMinSize = 5f;
    public float ZoomMaxSize = 30f;
    [SerializeField]
    private float zoomLerpSpeed = 20;
    private Vector3 dragOrigin;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }
        if (isLimitedCamPosition)
        {
            ClampCamera(); // limit the camera
        }
        if(ClampY)
        {
            ClampCameraY();
        }
        ZoomCamera();
        PanCamera(); // move the camera
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            //print($"origin {dragOrigin} new Position { cam.ScreenToWorldPoint(Input.mousePosition)} =difference {difference}");
            cam.transform.position += difference;

        }
    }

    private void ZoomCamera()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, ZoomMinSize, ZoomMaxSize);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }

    void ClampCamera()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraMinX, cameraMaxX),
            transform.position.y,
            Mathf.Clamp(transform.position.z, cameraMinZ, cameraMaxZ));
    }
    void ClampCameraY()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, cameraMinY, cameraMaxY), transform.position.z);
    }
}
