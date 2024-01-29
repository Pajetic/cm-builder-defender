using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

    private BuildingSO building;
    private int resourcePerInterval = 1;
    private float resourceTimer = 0f;
    private float resourceTimerMax;


    private void Awake() {
        building = GetComponent<BuildingBase>().Building;
        resourceTimerMax = building.resourceGeneratorData.TimerMax;
    }

    private void Update() {
        resourceTimer += Time.deltaTime;
        if (resourceTimer > resourceTimerMax) {
            resourceTimer = 0f;
            ResourceManager.Instance.AddResource(building.resourceGeneratorData.Resource, resourcePerInterval);
        }
    }

}
