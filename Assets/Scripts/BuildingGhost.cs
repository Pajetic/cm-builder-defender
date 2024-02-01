using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    [SerializeField] private Transform ghostSprite;

    private void Awake() {
        Hide();
    }

    private void Start() {
        BuildingManager.Instance.OnSelectedBuildingChanged += OnSelectedBuildingChanged;
    }

    private void OnSelectedBuildingChanged(object sender, BuildingManager.OnSelectedBuildingChangedEventArgs e) {
        BuildingSO selectedBuilding = e.SelectedBuilding;
        if (selectedBuilding != null) {
            Show(selectedBuilding.Sprite);
        } else {
            Hide();
        }
    }

    private void Update() {
        ghostSprite.position = GameInputManager.Instance.GetWorldMousePosition();
    }

    private void Show(Sprite sprite) {
        ghostSprite.GetComponent<SpriteRenderer>().sprite = sprite;
        ghostSprite.gameObject.SetActive(true);
    }

    private void Hide() {
        ghostSprite.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        BuildingManager.Instance.OnSelectedBuildingChanged -= OnSelectedBuildingChanged;
    }
}
