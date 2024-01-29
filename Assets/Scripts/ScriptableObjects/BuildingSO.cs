using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Building")]
public class BuildingSO : ScriptableObject {

    public string nameString;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;

}

