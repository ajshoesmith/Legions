using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CavalryType // cavalry types
{
    General,
    Melee
}



[CreateAssetMenu(fileName = "New Cavalry Object", menuName = "Unit System/Units/Cavalry")]
public class CavalryObject : UnitObject
{
    public CavalryType cavalryType;

    private void Awake()
    {
        type = UnitType.Cavalry;
        spriteResourcePath = "Sprites/Units/PurpleSolidCavalry";
        sprite = Resources.Load<Sprite>(spriteResourcePath);

        //Stats
        men = 500;
        armor = 0;
        atkDamage = 10;
        defDamage = 7;
        energy = 500;
        xp = 0;
        speed = 3f;
        turnSpeed = 180;
    }
}
