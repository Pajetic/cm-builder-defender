using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    [SerializeField] private Button soundIncreaseButton;
    [SerializeField] private Button soundDecreaseButton;
    [SerializeField] private Button musicIncreaseButton;
    [SerializeField] private Button musicDecreaseButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI soundVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;

    public void ToggleVisible() {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }

    private void Awake() {
        soundIncreaseButton.onClick.AddListener(() => {
            SoundManager.Instance.IncreaseVolume();
            UpdateVolumeText();
        });
        soundDecreaseButton.onClick.AddListener(() => {
            SoundManager.Instance.DecreaseVolume();
            UpdateVolumeText();
        });
        musicIncreaseButton.onClick.AddListener(() => {
            MusicManager.Instance.IncreaseVolume();
            UpdateVolumeText();
        });
        musicDecreaseButton.onClick.AddListener(() => {
            MusicManager.Instance.DecreaseVolume();
            UpdateVolumeText();
        });
        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
    }

    private void Start() {
        UpdateVolumeText();
        gameObject.SetActive(false);
    }

    private void UpdateVolumeText() {
        soundVolumeText.SetText(Mathf.RoundToInt(SoundManager.Instance.GetVolume() * 10).ToString());
        musicVolumeText.SetText(Mathf.RoundToInt(MusicManager.Instance.GetVolume() * 10).ToString());
    }
}
