using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance { get; private set; }

    private const string PREFS_MUSIC_VOLUME = "musicVolume";

    private AudioSource audioSource;
    private float volumeScale = 0.3f;

    public void IncreaseVolume() {
        volumeScale = Mathf.Clamp(volumeScale + 0.1f, 0, 1);
        audioSource.volume = volumeScale;
        PlayerPrefs.SetFloat(PREFS_MUSIC_VOLUME, volumeScale);

    }

    public void DecreaseVolume() {
        volumeScale = Mathf.Clamp(volumeScale - 0.1f, 0, 1);
        audioSource.volume = volumeScale;
        PlayerPrefs.SetFloat(PREFS_MUSIC_VOLUME, volumeScale);

    }

    public float GetVolume() {
        return volumeScale;
    }

    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volumeScale = PlayerPrefs.GetFloat(PREFS_MUSIC_VOLUME, volumeScale);
        audioSource.volume = volumeScale;
    }
}
