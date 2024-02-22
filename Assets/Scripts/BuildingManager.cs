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
    private const string BUILDING_ERROR_DIRECT_OVERLAP = "Area is not clear!";
    private const string BUILDING_ERROR_TOO_CLOSE = "Too close to another building of the same type!";
    private const string BUILDING_ERROR_TOO_FAR = "Too far from any other building!";
    private const string BUILDING_ERROR_CANNOT_AFFORD = "Cannot afford {0}";
    private const string BUILDING_NO_RESOURCE = "There are no nearby resources!";


    [SerializeField] private BuildingListSO buildingList;
    [SerializeField] private BuildingBase hqBuilding;
    [SerializeField] private GameObject constructionPrefab;
    private BuildingSO selectedBuilding;

    public void SetSelectedBuilding(BuildingSO building) {
        selectedBuilding = building;
        OnSelectedBuildingChanged?.Invoke(this, new OnSelectedBuildingChangedEventArgs { SelectedBuilding = this.selectedBuilding });
    }

    public BuildingBase GetHQBuilding() {
        return hqBuilding;
    }

    private void Awake() {
        Instance = this;
    }

    private void Start () {
        GameInputManager.Instance.OnMouse1 += OnMouse1;
        GameInputManager.Instance.OnSelectBuilding1 += OnSelectBuilding1;
        GameInputManager.Instance.OnSelectBuilding2 += OnSelectBuilding2;

        hqBuilding.GetComponent<HealthSystem>().OnDeath += HQBuilding_OnDeath;
    }

    private void HQBuilding_OnDeath(object sender, EventArgs e) {
        SoundManager.Instance.PlaySound(SoundManager.Sound.GameOver);
        GameOverUI.Instance.Show();
    }

    private void OnSelectBuilding2(object sender, System.EventArgs e) {
        selectedBuilding = buildingList.buildings[1];
    }

    private void OnSelectBuilding1(object sender, System.EventArgs e) {
        selectedBuilding = buildingList.buildings[0];
    }

    private void OnMouse1(object sender, System.EventArgs e) {
        Vector3 mousePosition = GameInputManager.Instance.GetMousePositionWorld();
        if (!EventSystem.current.IsPointerOverGameObject() && selectedBuilding != null) {
            if (CanSpawnBuilding(mousePosition, out string errorMessage)) {
                if (ResourceManager.Instance.TrySpendResource(selectedBuilding.ConstructionResourceCost)) {
                    SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                    Instantiate(constructionPrefab, mousePosition, Quaternion.identity).GetComponent<BuildingConstruction>().SetBuilding(selectedBuilding);
                } else {
                    TooltipUI.Instance.SetTooltipText(string.Format(BUILDING_ERROR_CANNOT_AFFORD, selectedBuilding.NameString), true);
                }
            } else {
                TooltipUI.Instance.SetTooltipText(errorMessage, true);
            }
        }
    }

    // Check if we can contrucst a building on mouse position
    private bool CanSpawnBuilding(Vector3 mousePosition, out string errorMessage) {
        // Check for direct overlap of nodes
        BoxCollider2D buildingCollider = selectedBuilding.Prefab.GetComponent<BoxCollider2D>();
        Collider2D[] colliderList = Physics2D.OverlapBoxAll(mousePosition + (Vector3)buildingCollider.offset, buildingCollider.size, 0f);
        if (colliderList.Length != 0) {
            errorMessage = BUILDING_ERROR_DIRECT_OVERLAP;
            return false;
        }

        if (selectedBuilding.CanGenerateResource) {
            if (Mathf.Approximately(ResourceGenerator.GetResourceEfficiency(mousePosition, selectedBuilding.ResourceGeneratorData), 0f)) {
                errorMessage = BUILDING_NO_RESOURCE;
                return false;
            }
        }

        // Check for being too close to same building type
        colliderList = Physics2D.OverlapCircleAll(mousePosition, selectedBuilding.SameBuildingRadiusRestriction);
        foreach(Collider2D node in colliderList) {
            BuildingBase buildingBase = node.GetComponent<BuildingBase>();
            if (buildingBase != null && buildingBase.Building == selectedBuilding) {
                errorMessage = BUILDING_ERROR_TOO_CLOSE;
                return false;
            }
        }

        // Check for being too far for any building
        colliderList = Physics2D.OverlapCircleAll(mousePosition, MAX_CONSTRUCTION_DISTANCE);
        foreach (Collider2D node in colliderList) {
            if (node.GetComponent<BuildingBase>() != null) {
                errorMessage = "";
                return true;
            }
        }
        errorMessage = BUILDING_ERROR_TOO_FAR;
        return false;
    }

    private void OnDestroy() {
        GameInputManager.Instance.OnMouse1 -= OnMouse1;
        GameInputManager.Instance.OnSelectBuilding1 -= OnSelectBuilding1;
        GameInputManager.Instance.OnSelectBuilding2 -= OnSelectBuilding2;
    }
}