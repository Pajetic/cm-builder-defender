using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager Instance {  get; private set; }

    public event EventHandler<OnSelectedBuildingChangedEventArgs> OnSelectedBuildingChanged;
    public class OnSelectedBuildingChangedEventArgs : EventArgs {
        public BuildingSO SelectedBuilding { get; set; }
    }

    [SerializeField] private BuildingListSO buildingList;
    private BuildingSO selectedBuilding;

    public void SetSelectedBuilding(BuildingSO building) {
        selectedBuilding = building;
        OnSelectedBuildingChanged?.Invoke(this, new OnSelectedBuildingChangedEventArgs { SelectedBuilding = this.selectedBuilding });
    }

    private void Awake() {
        Instance = this;
    }

    private void Start () {
        GameInputManager.Instance.OnMouse1 += OnMouse1;
        GameInputManager.Instance.OnSelectBuilding1 += OnSelectBuilding1;
        GameInputManager.Instance.OnSelectBuilding2 += OnSelectBuilding2;
    }

    private void OnSelectBuilding2(object sender, System.EventArgs e) {
        selectedBuilding = buildingList.buildings[1];
    }

    private void OnSelectBuilding1(object sender, System.EventArgs e) {
        selectedBuilding = buildingList.buildings[0];
    }

    private void OnMouse1(object sender, System.EventArgs e) {
        if (!EventSystem.current.IsPointerOverGameObject() && selectedBuilding != null) {
            Instantiate(selectedBuilding.Prefab, GameInputManager.Instance.GetWorldMousePosition(), Quaternion.identity);
        }
    }

    private void OnDestroy() {
        GameInputManager.Instance.OnMouse1 -= OnMouse1;
        GameInputManager.Instance.OnSelectBuilding1 -= OnSelectBuilding1;
        GameInputManager.Instance.OnSelectBuilding2 -= OnSelectBuilding2;
    }
}