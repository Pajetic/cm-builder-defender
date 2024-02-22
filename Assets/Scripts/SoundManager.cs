using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance {  get; private set; }

    public enum Sound {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        EnemyWaveStarting,
        GameOver
    }

    private const string PREFS_SFX_VOLUME = "sfxVolume";

    [SerializeField] private List<AudioClip> gameSFX;
    private AudioSource audioSource;
    private float volumeScale = 0.3f;

    public void IncreaseVolume() {
        volumeScale = Mathf.Clamp(volumeScale + 0.1f, 0, 1);
        PlayerPrefs.SetFloat(PREFS_SFX_VOLUME, volumeScale);
    }

    public void DecreaseVolume() {
        volumeScale = Mathf.Clamp(volumeScale - 0.1f, 0, 1);
        PlayerPrefs.SetFloat(PREFS_SFX_VOLUME, volumeScale);
    }

    public float GetVolume() {
        return volumeScale;
    }

    public void PlaySound(Sound sound) {
        audioSource.PlayOneShot(gameSFX[(int)sound], volumeScale);
    }

    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volumeScale = PlayerPrefs.GetFloat(PREFS_SFX_VOLUME, volumeScale);
    }

}
