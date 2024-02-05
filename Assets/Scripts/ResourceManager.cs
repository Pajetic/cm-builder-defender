using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourceAmountChanged;

    [SerializeField] private ResourceListSO resourceList;
    private Dictionary<ResourceSO, int> resourceAmountDict;

    public void AddResource(ResourceSO resource, int amount) {
        resourceAmountDict[resource] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanAfford(List<ResourceAmount> cost) {
        foreach(ResourceAmount resourceAmount in cost) {
            if (resourceAmountDict[resourceAmount.Resource] < resourceAmount.Amount) {
                return false;
            }
        }

        return true;
    }

    public bool TrySpendResource(List<ResourceAmount> cost) {
        if (!CanAfford(cost)) {
            return false;
        }

        foreach (ResourceAmount resourceAmount in cost) {
            resourceAmountDict[resourceAmount.Resource] -= resourceAmount.Amount;
        }

        return true;
    }

    public int GetResourceAmount(ResourceSO resource) {
        return resourceAmountDict[resource];
    }

    private void Awake() {
        Instance = this;

        resourceAmountDict = new Dictionary<ResourceSO, int>();

        foreach (ResourceSO resource in resourceList.resources) {
            resourceAmountDict[resource] = 0;
        }
    }
}

