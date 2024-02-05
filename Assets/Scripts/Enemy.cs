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
        finalTargetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        targetTransform = finalTargetTransform;
        findTargetTimerMax = Random.Range(findTargetTimerMax/2, findTargetTimerMax);
    }

    private void Update() {
        if (targetTransform == null) {
            if (finalTargetTransform == null) {
                rigidbody2D.velocity = Vector3.zero;
                return;
            }
            targetTransform = finalTargetTransform;
        }

        Vector3 moveVector = (targetTransform.position - transform.position).normalized;
        rigidbody2D.velocity = moveVector * moveSpeed;

        findTargetTimer += Time.deltaTime;
        if (findTargetTimer > findTargetTimerMax) {
            findTargetTimer = 0f;
            FindTarget();
        }
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
