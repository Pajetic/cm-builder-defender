using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyWaveManager : MonoBehaviour {

    public event EventHandler OnWaveChanged;

    private enum State {
        Waiting,
        Spawning
    }

    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private List<Transform> spawnPositionList;
    [SerializeField] private Transform nextWaveIndicator;
    private State state;
    private int waveNumber;
    private float waveTimer = 0f;
    [SerializeField] private float waveTimerMax = 10f;
    private float enemySpawnTimeVariance = 0.2f;
    private float enemySpawnTimer = 0f;
    private int enemiesToSpawn = 0;
    private Vector3 spawnPosition;

    public Vector3 GetSpawnPosition() {
        return spawnPosition;
    }
    
    public float GetNextWaveTimer() {
        return waveTimerMax - waveTimer;
    }

    public int GetWaveNumber() {
        return waveNumber;
    }

    public bool IsWaveSpawning() {
        return state == State.Spawning;
    }

    private void SpawnEnemy(Vector3 position) {
        Instantiate(enemyPrefab, position, Quaternion.identity).GetComponent<Enemy>();
    }

    private void Start() {
        PickNextSpawnPosition();
        state = State.Waiting;
    }

    private void Update() {
        switch (state) {
            case State.Waiting:
                waveTimer += Time.deltaTime;
                if (waveTimer > waveTimerMax) {
                    waveTimer = 0f;
                    SpawnWave();
                }
                break;
            case State.Spawning:
                if (enemiesToSpawn > 0) {
                    enemySpawnTimer -= Time.deltaTime;
                    if (enemySpawnTimer <= 0f) {
                        enemySpawnTimer = UnityEngine.Random.Range(0f, enemySpawnTimeVariance);
                        SpawnEnemy(spawnPosition + GameUtils.GetRandomDirection2D() * UnityEngine.Random.Range(0f, 10f));
                        enemiesToSpawn--;
                        if (enemiesToSpawn <= 0) {
                            PickNextSpawnPosition();
                            state = State.Waiting;
                        }
                    }
                } else {
                    state = State.Waiting;
                }
                break;

        }
    }

    private void SpawnWave() {
        enemiesToSpawn += 5 + 3 * waveNumber;
        state = State.Spawning;
        waveNumber++;
        OnWaveChanged?.Invoke(this, EventArgs.Empty);
    }

    private void PickNextSpawnPosition() {
        spawnPosition = spawnPositionList[UnityEngine.Random.Range(0, spawnPositionList.Count)].position;
        nextWaveIndicator.position = spawnPosition;
    }
}

