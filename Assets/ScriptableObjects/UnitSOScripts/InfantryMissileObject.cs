using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Archer Infantry Object", menuName = "Unit System/Units/Infantry/Archer Infantry")]
public class InfantryMissileObject : InfantryObject
{
    private void Awake()
    {
        infantryType = InfantryType.Missile;
        spriteResourcePath = "Sprites/Units/PurpleSolidArcher";
        sprite = Resources.Load<Sprite>(spriteResourcePath);

        //Stats
        men = 500;
        armor = 0;
        atkDamage = 2;
        defDamage = 4;
        energy = 100;
        xp = 0;
        speed = 2f;
        turnSpeed = 180;
    }
}
