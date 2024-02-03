using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplaySingle : MonoBehaviour {

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI amountLabel;

    public void SetResource(ResourceSO resource) {
        image.sprite = resource.Sprite;
    }

    public void SetAmount(int amount) {
        amountLabel.SetText(amount.ToString());
    }
}

