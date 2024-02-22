using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingBase : MonoBehaviour {

    [field: SerializeField] public BuildingSO Building { get; private set; }
    [SerializeField] private GameObject demolishButton;
    [SerializeField] private GameObject repairButton;
    [SerializeField] private GameObject buildingDestroyedParticle;
    private HealthSystem healthSystem;

    public void SetBuilding(BuildingSO building) {
        Building = building;
    }

    public void DestroyBuilding() {
        healthSystem.Kill();
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
        CinemachineShake.Instance.ShakeCamera(2f, 0.1f);
    }

    private void HealthSystem_OnDeath(object sender, System.EventArgs e) {
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        CinemachineShake.Instance.ShakeCamera(4f, 0.2f);
        Instantiate(buildingDestroyedParticle, transform.position, Quaternion.identity);
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
