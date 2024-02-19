using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour {

    [SerializeField] private float missileSpeed = 20f;
    [SerializeField] private int damage = 10;
    private Enemy targetEnemy;
    private Vector3 previousMoveDir;
    private float arrowDuration = 2f;

    public void SetTarget(Enemy enemy) {
        targetEnemy = enemy;
        Update();
    }

    private void Update() {
        Vector3 moveDir;
        if (targetEnemy == null) {
            moveDir = previousMoveDir;
        } else {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            previousMoveDir = moveDir;
        }
        transform.eulerAngles = new Vector3(0f, 0f, GameUtils.GetAngleFromVector(moveDir));
        transform.position += moveDir * missileSpeed * Time.deltaTime;

        arrowDuration -= Time.deltaTime;
        if (arrowDuration <= 0f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.gameObject.GetComponent<HealthSystem>().ApplyDamage(damage);
            Destroy(gameObject);
        }
    }
}
