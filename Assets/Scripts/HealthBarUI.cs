using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour {

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Transform barSpriteContainer;

    private void Start() {
        healthSystem.OnDamageTaken += OnHealthChanged;
        healthSystem.OnHealed += OnHealthChanged;
        UpdateBar();
    }

    private void OnHealthChanged(object sender, System.EventArgs e) {
        UpdateBar();
    }

    private void UpdateBar() {
        barSpriteContainer.localScale = new Vector3(healthSystem.GetHealthNormalized(), 1f, 1f);
        gameObject.SetActive(!healthSystem.IsFullHealth());
    }
}
