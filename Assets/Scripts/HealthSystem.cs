using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthSystem : MonoBehaviour {

    public event EventHandler OnDamageTaken;
    public event EventHandler OnDeath;
    public event EventHandler OnHealed;
    public event EventHandler OnMaxHealthChanged;

    [SerializeField] private int healthMax = 30;
    private int healthCurrent;

    public bool IsDead() {
        return healthCurrent <= 0;
    }

    public int GetHealth() {
        return healthCurrent;
    }

    public int GetHealthMax() {
        return healthMax;
    }

    public void SetHealthMax(int healthMax) {
        this.healthMax = healthMax;
        healthCurrent = healthMax;
        OnMaxHealthChanged?.Invoke(this, EventArgs.Empty);
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

    public void Kill() {
        ApplyDamage(healthCurrent);
    }

    public void Heal(int healAmount) {
        healthCurrent = Mathf.Clamp(healthCurrent + healAmount, 0, healthMax);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull() {
        healthCurrent = healthMax;
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public bool IsFullHealth() {
        return healthCurrent == healthMax;
    }

    private void Awake() {
        healthCurrent = healthMax;
    }

}
