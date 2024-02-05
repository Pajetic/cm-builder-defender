using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TooltipUI : MonoBehaviour {

    public static TooltipUI Instance {  get; private set; }

    [SerializeField] private TextMeshProUGUI tooltipText;
    private RectTransform rectTransform;
    private float tooltipTimer;
    private float tooltipTimerMax;

    public void SetTooltipText(string text, bool timed = false, int secondsToShow = 2) {
        tooltipText.SetText(text);
        SetPosition();
        if (timed) {
            tooltipTimer = 0f;
            tooltipTimerMax = secondsToShow;
        }
        Show();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void Awake() {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        Hide();
    }

    private void Update() {
        SetPosition();
        if (tooltipTimer < tooltipTimerMax) {
            tooltipTimer += Time.deltaTime;
            if (tooltipTimer > tooltipTimerMax) {
                Hide();
            }
        }
    }

    private void SetPosition() {
        Vector2 anchoredPosition = Mouse.current.position.ReadValue();

        anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0f, Screen.width - rectTransform.rect.width);
        anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, 0f, Screen.height - rectTransform.rect.height);

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void Show() {
        gameObject.SetActive(true);
    }
}
