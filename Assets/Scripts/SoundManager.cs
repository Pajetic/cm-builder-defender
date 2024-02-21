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

    [SerializeField] private List<AudioClip> gameSFX;
    private AudioSource audioSource;
    private float volumeScale = 0.3f;

    public void IncreaseVolume() {
        volumeScale = Mathf.Clamp(volumeScale + 0.1f, 0, 1);
    }

    public void DecreaseVolume() {
        volumeScale = Mathf.Clamp(volumeScale - 0.1f, 0, 1);
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
    }

}
