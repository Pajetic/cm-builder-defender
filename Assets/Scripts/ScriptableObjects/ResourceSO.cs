using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Resource")]
public class ResourceSO : ScriptableObject {

    public string NameString;
    public string NameStringShort;
    public Sprite Sprite;
    public string ColorHex;

}
