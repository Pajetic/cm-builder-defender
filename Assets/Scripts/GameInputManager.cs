using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour {

    public static GameInputManager Instance { get; private set; }

    public event EventHandler OnMouse1;
    public event EventHandler OnSelectBuilding1;
    public event EventHandler OnSelectBuilding2;

    private PlayerInputActions inputActions;

    public Vector3 GetMousePositionWorld() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0f;
        return mousePosition;
    }

    public Vector2 GetMousePositionScreen() {
        return Mouse.current.position.ReadValue();
    }

    public Vector2 GetCameraMovementVectorNormalized() {
        return inputActions.Player.CameraMovement.ReadValue<Vector2>();
    }

    public float GetMouseScrollDelta() {
        float scrollValue = inputActions.Player.CameraZoom.ReadValue<float>();
        // Scroll value on Windows has a bug where it returns +/- 120
        if (Mathf.Abs(scrollValue) > 1) {
            scrollValue /= 120;
        }
        return scrollValue;
    }

    private void Awake() {
        Instance = this;

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        inputActions.Player.Mouse1.performed += Mouse1_performed;
        inputActions.Player.SelectBuilding1.performed += SelectBuilding1_performed;
        inputActions.Player.SelectBuilding2.performed += SelectBuilding2_performed;
    }

    private void Mouse1_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnMouse1?.Invoke(this, EventArgs.Empty);
    }

    private void SelectBuilding1_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnSelectBuilding1?.Invoke(this, EventArgs.Empty);
    }

    private void SelectBuilding2_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnSelectBuilding2?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy() {
        inputActions.Player.Mouse1.performed -= Mouse1_performed;
        inputActions.Player.SelectBuilding1.performed -= SelectBuilding1_performed;
        inputActions.Player.SelectBuilding2.performed -= SelectBuilding2_performed;
        inputActions.Dispose();
    }
}

