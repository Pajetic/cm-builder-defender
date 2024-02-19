using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour {

    [SerializeField] private EnemyWaveManager enemyWaveManager;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI waveMessageText;
    [SerializeField] private RectTransform waveIndicatorSprite;
    [SerializeField] private RectTransform closestEnemyIndicatorSprite;
    private float waveIndicatorOffset = 300f;
    private float closestEnemyIndicatorOffset = 250f;
    private float closestEnemyRange = 500f;

    private void Start() {
        enemyWaveManager.OnWaveChanged += OnWaveChanged;
        SetWaveNumberText("");
    }

    private void OnWaveChanged(object sender, System.EventArgs e) {
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void Update() {

        HandleNextWaveMessage();
        HandleWaveIndicator();
        HandleClosestEnemyIndicator();
    }

    private void SetWaveNumberText(string waveNumberString) {
        waveNumberText.SetText(waveNumberString);
    }

    private void SetWaveMessage(string waveMessageString) {
        waveMessageText.SetText(waveMessageString);
    }

    private void HandleWaveIndicator() {
        Vector3 spawnPosition = enemyWaveManager.GetSpawnPosition();
        Vector3 spawnPositionVector = (spawnPosition - Camera.main.transform.position).normalized;
        waveIndicatorSprite.anchoredPosition = spawnPositionVector * waveIndicatorOffset;
        waveIndicatorSprite.eulerAngles = new Vector3(0, 0, GameUtils.GetAngleFromVector(spawnPositionVector));

        float cameraDistanceToWaveSpawnPosition = Vector2.Distance(spawnPosition, Camera.main.transform.position);
        waveIndicatorSprite.gameObject.SetActive(cameraDistanceToWaveSpawnPosition > Camera.main.orthographicSize);
    }

    private void HandleClosestEnemyIndicator() {
        BuildingBase hqBuilding = BuildingManager.Instance.GetHQBuilding();
        if (hqBuilding == null) {
            return;
        }
        Enemy closestEnemy = FindClosestEnemy(hqBuilding.transform.position);
        if (closestEnemy != null) {
            Vector3 closestEnemyPosition = closestEnemy.transform.position;
            Vector3 closestEnemyPositionVector = (closestEnemyPosition - Camera.main.transform.position).normalized;
            closestEnemyIndicatorSprite.anchoredPosition = closestEnemyPositionVector * closestEnemyIndicatorOffset;
            closestEnemyIndicatorSprite.eulerAngles = new Vector3(0, 0, GameUtils.GetAngleFromVector(closestEnemyPositionVector));

            float cameraDistanceToClosestEnemyPosition = Vector2.Distance(closestEnemyPosition, Camera.main.transform.position);
            closestEnemyIndicatorSprite.gameObject.SetActive(cameraDistanceToClosestEnemyPosition > Camera.main.orthographicSize);
        } else {
            closestEnemyIndicatorSprite.gameObject.SetActive(false);
        }
    }

    private Enemy FindClosestEnemy(Vector3 relativePosition) {
        Enemy closestEnemy = null;
        Collider2D[] nearbyNodes = Physics2D.OverlapCircleAll(relativePosition, closestEnemyRange);
        foreach (Collider2D node in nearbyNodes) {
            Enemy enemy = node.GetComponent<Enemy>();
            if (enemy != null) {
                if (closestEnemy == null) {
                    closestEnemy = enemy;
                } else {
                    if (Vector3.Distance(relativePosition, closestEnemy.transform.position) > Vector3.Distance(relativePosition, enemy.transform.position)) {
                        closestEnemy = enemy;
                    }
                }
            }
        }

        return closestEnemy;
    }

    private void HandleNextWaveMessage() {
        if (enemyWaveManager.IsWaveSpawning()) {
            SetWaveMessage("");
        } else {
            SetWaveMessage("Next wave in " + enemyWaveManager.GetNextWaveTimer().ToString("F1") + "s");
        }
    }
}