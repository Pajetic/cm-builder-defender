using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraHandler : MonoBehaviour {

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private float cameraMovementSensitivity = 10f;
    private float cameraZoomSensitivity = 2f;
    private float cameraZoomSpeed = 5f;
    private float cameraMinZoomDistance = 10f;
    private float cameraMaxZoomDistance = 30f;
    private float cameraOrthogrpahicSizeCurrent;
    private float cameraOrthogrpahicSizeTarget;

    private void Awake() {
        cameraOrthogrpahicSizeCurrent = virtualCamera.m_Lens.OrthographicSize;
        cameraOrthogrpahicSizeTarget = virtualCamera.m_Lens.OrthographicSize;
    }

    private void Update() {
        HandleCameraMovement();
        HandleCameraZoom();
    }

    private void HandleCameraMovement() {
        transform.position += GameInputManager.Instance.GetCameraMovementVectorNormalized() * Time.deltaTime * cameraMovementSensitivity;
    }

    private void HandleCameraZoom() {
        cameraOrthogrpahicSizeTarget = Mathf.Clamp(cameraOrthogrpahicSizeTarget + GameInputManager.Instance.GetMouseScrollDelta() * cameraZoomSensitivity,
            cameraMinZoomDistance, cameraMaxZoomDistance);
        if (!Mathf.Approximately(cameraOrthogrpahicSizeCurrent, cameraOrthogrpahicSizeTarget)) {
            cameraOrthogrpahicSizeCurrent = Mathf.Lerp(cameraOrthogrpahicSizeCurrent, cameraOrthogrpahicSizeTarget, Time.deltaTime * cameraZoomSpeed);
        }
        virtualCamera.m_Lens.OrthographicSize = cameraOrthogrpahicSizeCurrent;
    }
}
