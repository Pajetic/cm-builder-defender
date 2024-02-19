using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Building")]
public class BuildingSO : ScriptableObject {

    public string NameString;
    public Transform Prefab;
    public Sprite Sprite;
    public float SameBuildingRadiusRestriction;
    public bool CanGenerateResource;
    public ResourceGeneratorData ResourceGeneratorData;
    public int HealthMax;
    public List<ResourceAmount> ConstructionResourceCost;

    public string GetResourceCostString() {
        string costString = "";
        foreach (ResourceAmount resourceAmount in ConstructionResourceCost) {
            costString += "<color=#" + resourceAmount.Resource.ColorHex + ">" + resourceAmount.Resource.NameStringShort + resourceAmount.Amount + "</color>" + " ";
        }
        return costString;
    }

}

