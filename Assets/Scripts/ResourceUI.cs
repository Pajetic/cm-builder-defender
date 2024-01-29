using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUI : MonoBehaviour {

    [SerializeField] private ResourceListSO resourceList;
    [SerializeField] private Transform resourceTemplate;
    private Dictionary<ResourceSO, ResourceDisplaySingle> resourceDisplayDict;

    private void Awake() {
        resourceTemplate.gameObject.SetActive(false);
        resourceDisplayDict = new Dictionary<ResourceSO, ResourceDisplaySingle>();
    }

    private void Start() {
        SetupResources();

        ResourceManager.Instance.OnResourceAmountChanged += OnResourceAmountChanged;
    }

    private void OnResourceAmountChanged(object sender, System.EventArgs e) {
        UpdateResources();
    }

    private void SetupResources() {
        foreach(ResourceSO resource in resourceList.resources) {
            ResourceDisplaySingle resourceDisplay = Instantiate(resourceTemplate, transform).GetComponent<ResourceDisplaySingle>();
            resourceDisplay.SetResource(resource);
            resourceDisplay.SetAmount(ResourceManager.Instance.GetResourceAmount(resource));
            resourceDisplay.gameObject.SetActive(true);
            resourceDisplayDict.Add(resource, resourceDisplay);
        }
    }

    private void UpdateResources() {
        foreach (ResourceSO resource in resourceList.resources) {
            resourceDisplayDict[resource].SetResource(resource);
            resourceDisplayDict[resource].SetAmount(ResourceManager.Instance.GetResourceAmount(resource));
        }
    }
}