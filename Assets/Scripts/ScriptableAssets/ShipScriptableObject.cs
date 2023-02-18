using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShipScriptableObject", menuName = "ScriptableObject/ShipScriptableObject")]
public class ShipScriptableObject : ScriptableObject
{
    public int movingSpeed;
    public int rotatingSpeed;
    public float health;
}
