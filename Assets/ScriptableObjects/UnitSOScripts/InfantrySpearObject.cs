using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spear Infantry Object", menuName = "Unit System/Units/Infantry/Spear Infantry")]
public class InfantrySpearObject : InfantryObject
{
    private void Awake()
    {
        infantryType = InfantryType.Spear;
        spriteResourcePath = "Sprites/Units/PurpleSolidSpear";
        sprite = Resources.Load<Sprite>(spriteResourcePath);

        //Stats
        men = 1000;
        armor = 0;
        atkDamage = 3;
        defDamage = 10;
        energy = 100;
        xp = 0;
        speed = 1.5f;
        turnSpeed = 180;
    }
}
