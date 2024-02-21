using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressUI : MonoBehaviour {

    [SerializeField] private Image progressBar;
    [SerializeField] private BuildingConstruction buildingConstruction;

    private void Update() {
        progressBar.fillAmount = buildingConstruction.GetConstructionProgressNormalized();
    }


}