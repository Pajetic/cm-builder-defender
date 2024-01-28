using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    [SerializeField] private BuildingListSO buildingList;
    private BuildingSO selectedBuilding;

    private void Start () {
        GameInputManager.Instance.OnMouse1 += OnMouse1;
        GameInputManager.Instance.OnSelectBuilding1 += OnSelectBuilding1;
        GameInputManager.Instance.OnSelectBuilding2 += OnSelectBuilding2;

        selectedBuilding = buildingList.buildings[0];
    }

    private void OnSelectBuilding2(object sender, System.EventArgs e) {
        selectedBuilding = buildingList.buildings[1];
    }

    private void OnSelectBuilding1(object sender, System.EventArgs e) {
        selectedBuilding = buildingList.buildings[0];
    }

    private void OnMouse1(object sender, System.EventArgs e) {
        Instantiate(selectedBuilding.prefab, GameInputManager.Instance.GetWorldMousePosition(), Quaternion.identity);
    }
}