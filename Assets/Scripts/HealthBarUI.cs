using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour {

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Transform barSpriteContainer;
    [SerializeField] private Transform barSeparatorContainer;
    [SerializeField] private Transform barSeparatorTemplate;
    private float healthBarWidth = 3f;
    private float oneHealthWidth;
    private int barSeparatorAmount = 10;

    private void Start() {
        barSeparatorTemplate.gameObject.SetActive(false);
        SetUpSeparators();
        healthSystem.OnDamageTaken += OnHealthChanged;
        healthSystem.OnHealed += OnHealthChanged;
        healthSystem.OnMaxHealthChanged += OnMaxHealthChanged;
        UpdateBar();
    }

    private void OnMaxHealthChanged(object sender, System.EventArgs e) {
        SetUpSeparators();
    }

    private void OnHealthChanged(object sender, System.EventArgs e) {
        UpdateBar();
    }

    private void UpdateBar() {
        barSpriteContainer.localScale = new Vector3(healthSystem.GetHealthNormalized(), 1f, 1f);
        //gameObject.SetActive(!healthSystem.IsFullHealth());
        gameObject.SetActive(true);
    }

    private void SetUpSeparators() {
        foreach (Transform trans in barSeparatorContainer.transform) {
            if (trans != barSeparatorTemplate) {
                Destroy(trans.gameObject);
            }
        }

        oneHealthWidth = healthBarWidth / healthSystem.GetHealthMax();
        int separatorCount = Mathf.FloorToInt(healthSystem.GetHealthMax() / barSeparatorAmount);

        for (int i = 1; i <= separatorCount; i++) {
            Transform separator = Instantiate(barSeparatorTemplate, barSeparatorContainer);
            separator.localPosition = new Vector3(oneHealthWidth * barSeparatorAmount * i, 0, 0);
            separator.gameObject.SetActive(true);
        }
    }
}
