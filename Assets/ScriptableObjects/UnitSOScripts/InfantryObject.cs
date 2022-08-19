using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfantryType    // infantry types
{
    Melee,
    Spear,
    Missile
}

[CreateAssetMenu(fileName = "New Infantry Object", menuName = "Unit System/Units/Infantry/Default Infantry")]
public class InfantryObject : UnitObject
{
    public InfantryType infantryType;
    
    

    private void Awake()
    {
        type = UnitType.Infantry;

        //Stats
        men = 1000;
        armor = 0;
        atkDamage = 5;
        defDamage = 7;
        energy = 100;
        xp = 0;
        speed = 1.5f;
        turnSpeed = 180;


        
        spriteResourcePath = "Sprites/Units/PurpleSolidLegate";
        //infantrySpriteArray = Resources.LoadAll<Sprite>("PlaceholderPurpleMelee, PlaceholderPurpleSpear");
        sprite = Resources.Load<Sprite>(spriteResourcePath);
    }
}