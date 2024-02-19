using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float targetAcquisitionRadius = 30f;
    [SerializeField] private GameObject attackProjetile;
    [SerializeField] private Transform projectileSpawn;
    private Enemy targetEnemy;
    private float findTargetTimer = 0f;
    private float findTargetTimerMax = 0.2f;
    private float attackTimer = 0f;
    [SerializeField] private float attackTimerMax = 1f;

    private void Start() {
        findTargetTimerMax = Random.Range(findTargetTimerMax / 2, findTargetTimerMax);
    }

    private void Update() {
        HandleTargeting();
        HandleAttacking();
    }

    private void HandleTargeting() {
         findTargetTimer += Time.deltaTime;
        if (findTargetTimer > findTargetTimerMax) {
            findTargetTimer = 0f;
            FindTarget();
        }
    }

    private void HandleAttacking() {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackTimerMax) {
            attackTimer = 0;
            if (targetEnemy != null) {
                ArrowProjectile arrowProjectile = Instantiate(attackProjetile, projectileSpawn.position, Quaternion.identity).GetComponent<ArrowProjectile>();
                arrowProjectile.SetTarget(targetEnemy);
            }
        }
    }

    private void FindTarget() {
        Collider2D[] nearbyNodes = Physics2D.OverlapCircleAll(transform.position, targetAcquisitionRadius);
        foreach (Collider2D node in nearbyNodes) {
            Enemy enemy = node.GetComponent<Enemy>();
            if (enemy != null) {
                if (targetEnemy == null) {
                    targetEnemy = enemy;
                } else {
                    if (Vector3.Distance(transform.position, targetEnemy.transform.position) > Vector3.Distance(transform.position, enemy.transform.position)) {
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }
}
