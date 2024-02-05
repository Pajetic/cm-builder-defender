using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner Instance {  get; private set; }

    [SerializeField] private Transform enemyPrefab;

    public Enemy SpawnEnemy(Vector3 position) {
        return Instantiate(enemyPrefab, position, Quaternion.identity).GetComponent<Enemy>();
    }

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (Keyboard.current.qKey.wasPressedThisFrame) {
            SpawnEnemy(GameInputManager.Instance.GetMousePositionWorld());
        }
    }

}
