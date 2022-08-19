using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Army", menuName = "Unit System/Army")]
public class ArmyObject : ScriptableObject
{
    public List<UnitObject> unitList = new List<UnitObject>();

    private void Awake()
    {
        
        
    }
}
