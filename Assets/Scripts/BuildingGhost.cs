using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingGhost : MonoBehaviour {

    [SerializeField] private Transform ghostSprite;
    [SerializeField] private Transform efficiencyOverlay;
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
            Show();
        } else {
            Hide();
        }
    }

    private void Update() {
        transform.position = GameInputManager.Instance.GetMousePositionWorld();
        if (selectedBuilding != null && selectedBuilding.CanGenerateResource) {
            resourceEfficiency.SetText((ResourceGenerator.GetResourceEfficiency(transform.position, selectedBuilding.ResourceGeneratorData) * 100).ToString("F0") + "%");
        }
    }

    private void Show() {
        ghostSprite.GetComponent<SpriteRenderer>().sprite = selectedBuilding.Sprite;
        ghostSprite.gameObject.SetActive(true);
        if (selectedBuilding.CanGenerateResource) {
            resourceIcon.sprite = selectedBuilding.ResourceGeneratorData.Resource.Sprite;
            efficiencyOverlay.gameObject.SetActive(true);
        } else {
            efficiencyOverlay.gameObject.SetActive(false);
        }
    }

    private void Hide() {
        ghostSprite.gameObject.SetActive(false);
        efficiencyOverlay.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        BuildingManager.Instance.OnSelectedBuildingChanged -= OnSelectedBuildingChanged;
    }
}
