using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShake : MonoBehaviour {

    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineMultiChannelPerlin;
    private float shakeTimer;
    private float shakeTimerMax;
    private float shakeIntensity;

    public void ShakeCamera(float intensity, float timer) {
        shakeTimerMax = timer;
        shakeTimer = 0f;
        shakeIntensity = intensity;
    }

    private void Awake() {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update() {
        if (shakeTimer < shakeTimerMax) {
            shakeTimer += Time.deltaTime;
            cinemachineMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(shakeIntensity, 0f, shakeTimer / shakeTimerMax);
        }
    } 
}