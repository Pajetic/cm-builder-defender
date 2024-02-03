using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGeneratorUI : MonoBehaviour {

    [SerializeField] private ResourceGenerator resourceGenerator;
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TextMeshProUGUI resourceTimer;
    [SerializeField] private Image progressBar;

    private void Awake() {
        progressBar.fillAmount = 0f;
    }

    private void Start() {
        resourceIcon.sprite = resourceGenerator.GetResourceIcon();
    }

    private void Update() {
        resourceTimer.SetText(resourceGenerator.GetResourcePerSecond().ToString("#.#"));
        progressBar.fillAmount = resourceGenerator.GetResourceProgressNormalized();
    }
}