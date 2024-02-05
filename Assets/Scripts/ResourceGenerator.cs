using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

    private ResourceGeneratorData resourceGeneratorData;
    private int resourcePerInterval = 1;
    private float resourceTimer = 0f;
    private float resourceTimerMax;

    public static float GetResourceEfficiency(Vector3 position, ResourceGeneratorData resourceGeneratorData) {
        Collider2D[] nodes = Physics2D.OverlapCircleAll(position, resourceGeneratorData.ResourceDetectionRadius);

        int nearbyResourceCount = 0;
        foreach (Collider2D node in nodes) {
            ResourceNode resourceNode = node.GetComponent<ResourceNode>();
            if (resourceNode != null && resourceNode.Resource == resourceGeneratorData.Resource) {
                nearbyResourceCount++;
            }
        }
        nearbyResourceCount = Mathf.Clamp(nearbyResourceCount, 0, resourceGeneratorData.MaxResourceNodeCount);
        return (float)nearbyResourceCount / resourceGeneratorData.MaxResourceNodeCount;
    }

    public Sprite GetResourceIcon() {
        return resourceGeneratorData.Resource.Sprite;
    }

    public float GetResourcePerSecond() {
        return 1 / resourceTimerMax;
    }

    public float GetResourceProgressNormalized() {
        return resourceTimer / resourceTimerMax;
    }

    private void Awake() {
        resourceGeneratorData = GetComponent<BuildingBase>().Building.ResourceGeneratorData;
        resourceTimerMax = resourceGeneratorData.TimerMax;
    }

    private void Start() {
        CalculateResourceGeneration();
    }

    private void Update() {
        resourceTimer += Time.deltaTime;
        if (resourceTimer > resourceTimerMax) {
            resourceTimer = 0f;
            ResourceManager.Instance.AddResource(resourceGeneratorData.Resource, resourcePerInterval);
        }
    }

    private void CalculateResourceGeneration() {
        float gatheringEfficiency = GetResourceEfficiency(transform.position, resourceGeneratorData);

        if (gatheringEfficiency == 0) {
            enabled = false;
        } else {
            resourceTimerMax = resourceTimerMax / 2f + resourceTimerMax * (1 - gatheringEfficiency);
        }
    }
}
