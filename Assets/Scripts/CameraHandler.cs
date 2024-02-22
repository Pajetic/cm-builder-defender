using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraHandler : MonoBehaviour {

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private PolygonCollider2D cameraBounds;
    private float cameraMovementSensitivity = 40f;
    private float cameraZoomSensitivity = 2f;
    private float cameraZoomSpeed = 5f;
    private float cameraMinZoomDistance = 10f;
    private float cameraMaxZoomDistance = 30f;
    private float cameraOrthogrpahicSizeCurrent;
    private float cameraOrthogrpahicSizeTarget;
    private float edgeScrollingBounds = 20f;

    private void Awake() {
        cameraOrthogrpahicSizeCurrent = virtualCamera.m_Lens.OrthographicSize;
        cameraOrthogrpahicSizeTarget = virtualCamera.m_Lens.OrthographicSize;
    }

    private void Update() {
        HandleCameraMovement();
        HandleCameraZoom();
    }

    private void HandleCameraMovement() {
        Vector2 movementVector = new Vector2(0, 0);
        Vector2 mousePosition = GameInputManager.Instance.GetMousePositionScreen();

        if (mousePosition.x > Screen.width - edgeScrollingBounds) {
            movementVector.x = 1f;
        }
        if (mousePosition.x < edgeScrollingBounds) {
            movementVector.x = -1f;
        }
        if (mousePosition.y > Screen.height - edgeScrollingBounds) {
            movementVector.y = 1f;
        }
        if (mousePosition.y < edgeScrollingBounds) {
            movementVector.y = -1f;
        }

        Vector3 newPosition = transform.position + (Vector3)(movementVector.magnitude > 0 ? movementVector.normalized : GameInputManager.Instance.GetCameraMovementVectorNormalized()) * Time.deltaTime * cameraMovementSensitivity;
        if (cameraBounds.bounds.Contains(newPosition)) {
            transform.position = newPosition;
        }
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
