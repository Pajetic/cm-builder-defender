using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingGhost : MonoBehaviour {

    [SerializeField] private Transform visualContainer;
    [SerializeField] private Transform ghostSprite;
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TextMeshProUGUI resourceEfficiency;
    private BuildingSO selectedBuilding;

    private void Awake() {
        Hide();
    }

    private void Start() {
        BuildingManager.Instance.OnSelectedBuildingChanged += OnSelectedBuildingChanged;
    }

    private void OnSelectedBuildingChanged(object sender, BuildingManager.OnSelectedBuildingChangedEventArgs e) {
        selectedBuilding = e.SelectedBuilding;
        if (selectedBuilding != null) {
            resourceIcon.sprite = selectedBuilding.ResourceGeneratorData.Resource.Sprite;
            Show(selectedBuilding.Sprite);
        } else {
            Hide();
        }
    }

    private void Update() {
        transform.position = GameInputManager.Instance.GetMousePositionWorld();
        if (selectedBuilding != null) {
            resourceEfficiency.SetText((ResourceGenerator.GetResourceEfficiency(transform.position, selectedBuilding.ResourceGeneratorData) * 100).ToString("F0") + "%");
        }
    }

    private void Show(Sprite sprite) {
        ghostSprite.GetComponent<SpriteRenderer>().sprite = sprite;
        visualContainer.gameObject.SetActive(true);
    }

    private void Hide() {
        visualContainer.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        BuildingManager.Instance.OnSelectedBuildingChanged -= OnSelectedBuildingChanged;
    }
}
