using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthSystem : MonoBehaviour {

    public event EventHandler OnDamageTaken;
    public event EventHandler OnDeath;

    [SerializeField] private int healthMax;
    private int healthCurrent;

    public bool IsDead() {
        return healthCurrent <= 0;
    }

    public int GetHealth() {
        return healthCurrent;
    }

    public void SetHealthMax(int healthMax) {
        this.healthMax = healthMax;
        healthCurrent = healthMax;
    }

    public float GetHealthNormalized() {
        return (float)healthCurrent / healthMax;
    }

    public void ApplyDamage(int damage) {
        healthCurrent = Mathf.Clamp(healthCurrent - damage, 0, healthMax);
        OnDamageTaken?.Invoke(this, EventArgs.Empty);
        if (IsDead()) {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsFullHealth() {
        return healthCurrent == healthMax;
    }

    private void Awake() {
        healthCurrent = healthMax;
    }

}
