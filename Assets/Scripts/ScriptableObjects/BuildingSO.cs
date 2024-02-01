using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Building")]
public class BuildingSO : ScriptableObject {

    public string NameString;
    public Transform Prefab;
    public Sprite Sprite;
    public ResourceGeneratorData ResourceGeneratorData;

}

