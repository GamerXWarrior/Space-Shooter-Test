using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObjectList", menuName = "ScriptableObject/WeaponScriptableObjectList")]
public class WeaponScriptableObjectList : ScriptableObject
{
    public WeaponScriptableObject[] weaponScriptableObject;
}
