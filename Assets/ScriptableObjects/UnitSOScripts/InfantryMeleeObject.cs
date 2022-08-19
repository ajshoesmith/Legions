using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Infantry Object", menuName = "Unit System/Units/Infantry/Melee Infantry")]
public class InfantryMeleeObject : InfantryObject
{
    private void Awake()
    {
        infantryType = InfantryType.Melee;
        spriteResourcePath = "Sprites/Units/PurpleSolidMelee";
        sprite = Resources.Load<Sprite>(spriteResourcePath);

        //Stats
        men = 1000;
        armor = 0;
        atkDamage = 5;
        defDamage = 7;
        energy = 100;
        xp = 0;
        speed = 1.5f;
        turnSpeed = 180;
    }
}
