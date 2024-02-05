using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingBase : MonoBehaviour {

    [field: SerializeField] public BuildingSO Building { get; private set; }
    private HealthSystem healthSystem;

    private void Start() {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthMax(Building.HealthMax);
        healthSystem.OnDeath += HealthSystem_OnDeath;
    }

    private void HealthSystem_OnDeath(object sender, System.EventArgs e) {
        Destroy(gameObject);
    }
}
