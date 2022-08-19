using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Artillery Object", menuName = "Unit System/Units/Artillery")]
public class ArtilleryObject : UnitObject
{
    public void Awake()
    {
        type = UnitType.Artillery;
    }
}