using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour {

    private const string CANNOT_AFFORD_REPAIR = "Cannot afford repair cost!";

    [SerializeField] private Button button;
    [SerializeField] private HealthSystem buildingHealthSystem;
    [SerializeField] private ResourceSO repairResource;

    private void Awake() {
        button.onClick.AddListener(() => {
            List<ResourceAmount> repairCost = new List<ResourceAmount> {
                new ResourceAmount { Resource = repairResource, Amount = (buildingHealthSystem.GetHealthMax() - buildingHealthSystem.GetHealth()) / 2 } };

            if (ResourceManager.Instance.CanAfford(repairCost)) {
                ResourceManager.Instance.TrySpendResource(repairCost);
                buildingHealthSystem.HealFull();
            } else {
                TooltipUI.Instance.SetTooltipText(CANNOT_AFFORD_REPAIR, true);
            }
        });
    }

}
