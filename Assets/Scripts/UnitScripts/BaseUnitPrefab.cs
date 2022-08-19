using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitPrefab : MonoBehaviour
{
    public UnitObject unitObject;
    public ArmyObject armyObject;
    private SpriteRenderer spriteR;
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.sprite = unitObject.sprite;
        gameObject.name = unitObject.name;
    }

        // Update is called once per frame
    void Update()
    {

    }
 }

