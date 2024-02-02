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

    // Max distance a building has to be from another building
    private const float MAX_CONSTRUCTION_DISTANCE = 20f;

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
        Vector3 mousePosition = GameInputManager.Instance.GetMousePositionWorld();
        if (!EventSystem.current.IsPointerOverGameObject() && selectedBuilding != null && CanSpawnBuilding(mousePosition)) {
            Instantiate(selectedBuilding.Prefab, mousePosition, Quaternion.identity);
        }
    }

    // Check if we can contrucst a building on mouse position
    private bool CanSpawnBuilding(Vector3 mousePosition) {
        // Check for direct overlap of nodes
        BoxCollider2D buildingCollider = selectedBuilding.Prefab.GetComponent<BoxCollider2D>();
        Collider2D[] colliderList = Physics2D.OverlapBoxAll(mousePosition + (Vector3)buildingCollider.offset, buildingCollider.size, 0f);
        if (colliderList.Length != 0) {
            return false;
        }

        // Check for being too close to same building type
        colliderList = Physics2D.OverlapCircleAll(mousePosition, selectedBuilding.SameBuildingRadiusRestriction);
        foreach(Collider2D node in colliderList) {
            BuildingBase buildingBase = node.GetComponent<BuildingBase>();
            if (buildingBase != null && buildingBase.Building == selectedBuilding) {
                return false;
            }
        }

        // Check for being too far for any building
        colliderList = Physics2D.OverlapCircleAll(mousePosition, MAX_CONSTRUCTION_DISTANCE);
        foreach (Collider2D node in colliderList) {
            if (node.GetComponent<BuildingBase>() != null) {
                return true;
            }
        }

        return false;
    }

    private void OnDestroy() {
        GameInputManager.Instance.OnMouse1 -= OnMouse1;
        GameInputManager.Instance.OnSelectBuilding1 -= OnSelectBuilding1;
        GameInputManager.Instance.OnSelectBuilding2 -= OnSelectBuilding2;
    }
}