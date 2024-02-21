using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    public static GameOverUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI waveSurvivedText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        Instance = this;

        Hide();

        retryButton.onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });
        mainMenuButton.onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
    }

    public void Show() {
        waveSurvivedText.SetText("You Survived " + EnemyWaveManager.Instance.GetWaveNumber() + " Waves!");
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
