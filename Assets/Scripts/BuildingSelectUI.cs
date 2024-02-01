using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectUI : MonoBehaviour {

    [SerializeField] private BuildingListSO buildingList;
    [SerializeField] private Transform buildingButtonTemplate;
    [SerializeField] private Transform cursorButtonTransform;
    private List<BuildingButtonTemplate> buildingButtonList = new List<BuildingButtonTemplate>();

    private void Awake() {
        buildingButtonTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        SetUpClearSelectButton();
        SetUpBuildingButtons();
    }

    private void SetUpClearSelectButton() {
        BuildingButtonTemplate cursorButton = cursorButtonTransform.GetComponent<BuildingButtonTemplate>();
        cursorButton.SetSelected(true);
        cursorButton.OnClick += BuildingButton_OnClick;
        cursorButton.gameObject.SetActive(true);
        buildingButtonList.Add(cursorButton);
    }

    private void SetUpBuildingButtons() {
        foreach (BuildingSO buildingSO in buildingList.buildings) {
            BuildingButtonTemplate buildingButton = Instantiate(buildingButtonTemplate, transform).GetComponent<BuildingButtonTemplate>();
            buildingButton.SetBuilding(buildingSO);
            buildingButton.OnClick += BuildingButton_OnClick;
            buildingButton.gameObject.SetActive(true);
            buildingButtonList.Add(buildingButton);
        }
    }

    private void BuildingButton_OnClick(object sender, System.EventArgs e) {
        BuildingButtonTemplate selectedButton = sender as BuildingButtonTemplate;
        BuildingManager.Instance.SetSelectedBuilding(selectedButton.GetBuilding());
        foreach(BuildingButtonTemplate buildingButton in buildingButtonList) {
            buildingButton.SetSelected(buildingButton == selectedButton);
        }
    }
}
