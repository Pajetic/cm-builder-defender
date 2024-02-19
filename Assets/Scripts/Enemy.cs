using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour {

    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float targetAcquisitionRadius = 10f;
    new private Rigidbody2D rigidbody2D;
    private Transform finalTargetTransform;
    private Transform targetTransform;
    private float findTargetTimerMax = 0.2f;
    private float findTargetTimer = 0f;

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        BuildingBase hq = BuildingManager.Instance.GetHQBuilding();
        finalTargetTransform = hq == null ? null : hq.transform;
        targetTransform = finalTargetTransform;
        findTargetTimerMax = Random.Range(findTargetTimerMax/2, findTargetTimerMax);
        gameObject.GetComponent<HealthSystem>().OnDeath += OnDeath;
    }

    private void OnDeath(object sender, System.EventArgs e) {
        Destroy(gameObject);
    }

    private void Update() {
        HandleTargeting();
        HandleMovement();
    }

    private void HandleTargeting() {
        if (finalTargetTransform == null) {
            return;
        }

        if (targetTransform == null) {
            targetTransform = finalTargetTransform;
        }

        findTargetTimer += Time.deltaTime;
        if (findTargetTimer > findTargetTimerMax) {
            findTargetTimer = 0f;
            FindTarget();
        }
    }

    private void HandleMovement() {
        if (targetTransform == null) {
            rigidbody2D.velocity = Vector3.zero;
            return;
        }
        Vector3 moveVector = (targetTransform.position - transform.position).normalized;
        rigidbody2D.velocity = moveVector * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        BuildingBase building = collision.gameObject.GetComponent<BuildingBase>();
        if (building != null) {
            building.GetComponent<HealthSystem>().ApplyDamage(damage);
            Destroy(gameObject);
        }
    }

    private void FindTarget() {
        Collider2D[] nearbyNodes = Physics2D.OverlapCircleAll(transform.position, targetAcquisitionRadius);
        foreach (Collider2D node in nearbyNodes) {
            BuildingBase building = node.GetComponent<BuildingBase>();
            if (building != null) {
                if (targetTransform == null) {
                    targetTransform = building.transform;
                } else {
                    if (Vector3.Distance(transform.position, targetTransform.position) > Vector3.Distance(transform.position, building.transform.position)) {
                        targetTransform = building.transform;
                    }
                }
            }
        }
    }
}
