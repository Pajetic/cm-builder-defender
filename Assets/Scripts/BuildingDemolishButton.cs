using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour {

    [SerializeField] private Button button;
    [SerializeField] private BuildingBase building;
    private float deconstructionRefundMultiplier = 0.6f;

    private void Awake() {
        button.onClick.AddListener(() => {
            BuildingSO buildingType = building.GetComponent<BuildingBase>().Building;
            foreach(ResourceAmount resourceAmount in buildingType.ConstructionResourceCost) {
                ResourceManager.Instance.AddResource(resourceAmount.Resource, Mathf.FloorToInt(resourceAmount.Amount * deconstructionRefundMultiplier));
            }
            Destroy(building.gameObject);
        });
    }

}
