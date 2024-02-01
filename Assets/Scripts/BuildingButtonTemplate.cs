using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonTemplate : MonoBehaviour {

    public event EventHandler OnClick;

    [SerializeField] private Image buildingIcon;
    [SerializeField] private Image selectedBorder;
    private BuildingSO building;

    public void SetBuilding(BuildingSO building) {
        this.building = building;
        buildingIcon.sprite = building.Sprite;
    }

    public BuildingSO GetBuilding() {
        return building;
    }

    public void SetSelected(bool selected) {
        selectedBorder.gameObject.SetActive(selected);
    }

    private void Awake() {
        selectedBorder.gameObject.SetActive(false);

        gameObject.GetComponent<Button>().onClick.AddListener(() => { OnClick?.Invoke(this, EventArgs.Empty); });
    }
}
