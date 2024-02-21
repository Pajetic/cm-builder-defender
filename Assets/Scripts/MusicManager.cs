using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float volumeScale = 0.3f;

    public void IncreaseVolume() {
        volumeScale = Mathf.Clamp(volumeScale + 0.1f, 0, 1);
        audioSource.volume = volumeScale;
    }

    public void DecreaseVolume() {
        volumeScale = Mathf.Clamp(volumeScale - 0.1f, 0, 1);
        audioSource.volume = volumeScale;
    }

    public float GetVolume() {
        return volumeScale;
    }

    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volumeScale;
    }
}
