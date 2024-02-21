using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingBase : MonoBehaviour {

    [field: SerializeField] public BuildingSO Building { get; private set; }
    [SerializeField] private GameObject demolishButton;
    [SerializeField] private GameObject repairButton;
    private HealthSystem healthSystem;

    public void SetBuilding(BuildingSO building) {
        Building = building;
    }

    private void Start() {
        healthSystem = GetComponent<HealthSystem>();
        if (healthSystem != null) {
            healthSystem.SetHealthMax(Building.HealthMax);
            healthSystem.OnDeath += HealthSystem_OnDeath;
        }

        HideDemolishButton();
        HideRepairButton();

        healthSystem.OnDamageTaken += HealthSystem_OnDamageTaken;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e) {
        if (healthSystem.IsFullHealth()) {
            HideRepairButton();
        }
    }

    private void HealthSystem_OnDamageTaken(object sender, System.EventArgs e) {
        ShowRepairButton();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
    }

    private void HealthSystem_OnDeath(object sender, System.EventArgs e) {
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        Destroy(gameObject);
    }

    private void OnMouseEnter() {
        ShowDemolishButton();
    }

    private void OnMouseExit() {
        HideDemolishButton();
    }

    private void ShowDemolishButton() {
        if (demolishButton != null) {
            demolishButton.SetActive(true);
        }
    }

    private void HideDemolishButton() {
        if(demolishButton != null) {
            demolishButton.SetActive(false);
        }
    }

    private void ShowRepairButton() {
        if (repairButton != null) {
            repairButton.SetActive(true);
        }
    }

    private void HideRepairButton() {
        if (repairButton != null) {
            repairButton.SetActive(false);
        }
    }
}
