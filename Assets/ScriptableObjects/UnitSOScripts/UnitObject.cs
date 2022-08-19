using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType    // unit types
{
    Infantry,
    Cavalry,
    Artillery
}

public abstract class UnitObject : ScriptableObject
{
    public UnitType type;                                       // Unit class 
    public Vector3 spawnPosition;                               // Vector for spawn position (Starting Formation)
    [TextArea(15,20)]                                           // easier to read desc in unity editor
    public string description;                                  // Self Explainatory
    [SerializeField] public ArmyObject armyObject;              // Army Scriptable Object 
    public string spriteResourcePath;                           // Path to Sprite
    public Sprite sprite;                                       // sprite

    //Stats
    public int men;
    public float armor;
    public float atkDamage;
    public float defDamage;
    public float energy;
    public float xp;
    public float speed;
    public float turnSpeed;

    private void Awake()
    {
        
    }
}
