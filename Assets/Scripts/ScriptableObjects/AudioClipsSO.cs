using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AudioClipsSO")]
public class AudioClipsSO : ScriptableObject {

    public AudioClip BuildingDamaged;
    public AudioClip BuildingDestroyed;
    public AudioClip BuildingPlaced;
    public AudioClip EnemyDie;
    public AudioClip EnemyHit;
    public AudioClip EnemyWaveStarting;
    public AudioClip GameOver;

}
