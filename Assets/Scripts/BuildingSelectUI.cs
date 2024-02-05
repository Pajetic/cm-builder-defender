using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectUI : MonoBehaviour {

    private const string CURSOR_TOOLTIP = "Cursor";

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
        cursorButton.GetComponent<MouseoverEvents>().OnMouseEnter += (sender, eventArgs) => {
            TooltipUI.Instance.SetTooltipText(CURSOR_TOOLTIP);
        };
        cursorButton.GetComponent<MouseoverEvents>().OnMouseExit += (sender, eventArgs) => {
            TooltipUI.Instance.Hide();
        };
        cursorButton.gameObject.SetActive(true);
        buildingButtonList.Add(cursorButton);
    }

    private void SetUpBuildingButtons() {
        foreach (BuildingSO buildingSO in buildingList.buildings) {
            BuildingButtonTemplate buildingButton = Instantiate(buildingButtonTemplate, transform).GetComponent<BuildingButtonTemplate>();
            buildingButton.SetBuilding(buildingSO);
            buildingButton.OnClick += BuildingButton_OnClick;
            buildingButton.GetComponent<MouseoverEvents>().OnMouseEnter += (sender, eventArgs) => {
                TooltipUI.Instance.SetTooltipText(buildingSO.NameString + "\n" + buildingSO.GetResourceCostString());
            };
            buildingButton.GetComponent<MouseoverEvents>().OnMouseExit += (sender, eventArgs) => {
                TooltipUI.Instance.Hide();
            };
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
