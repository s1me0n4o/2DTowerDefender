using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private Camera mainCam;
    private float _orthographicSize;
    private float _targetOrthograpicSize;
    private Vector3 _touchStart;

    private void Start()
    {
        mainCam = Camera.main;
        _orthographicSize = virtualCamera.m_Lens.OrthographicSize;
        _targetOrthograpicSize = _orthographicSize;
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        HandleCameraMovement();
        HandleCameraZoom();
    }

    private void HandleCameraZoom()
    {
        //camera zoom
        var zoomAount = 2f;
        _targetOrthograpicSize -= Input.mouseScrollDelta.y * zoomAount;

        var minOrthoSize = 10f;
        var maxOrthoSize = 30f;
        _targetOrthograpicSize = Mathf.Clamp(_targetOrthograpicSize, minOrthoSize, maxOrthoSize);

        //camera smootness
        var zoomSpeed = 5f;
        _orthographicSize = Mathf.Lerp(_orthographicSize, _targetOrthograpicSize, Time.deltaTime * zoomSpeed);

        virtualCamera.m_Lens.OrthographicSize = _orthographicSize;
    }

    private void HandleCameraMovement()
    {
        if (Input.GetMouseButtonDown(0) && BuildingManager.Instance.GetActiveBuildingType() == null)
        {
            _touchStart = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && BuildingManager.Instance.GetActiveBuildingType() == null)
        {
            var moveDirection = _touchStart - mainCam.ScreenToWorldPoint(Input.mousePosition);
            var cameraMoveSpeed = 10;
            transform.position += (Vector3)moveDirection * cameraMoveSpeed * Time.deltaTime;
            var xClamp = Mathf.Clamp(transform.position.x, -45, 45);
            var yClamp = Mathf.Clamp(transform.position.y, -60, 60);
            transform.position = new Vector3(xClamp, yClamp, 0);
        }
    }
}
