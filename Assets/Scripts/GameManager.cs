using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            //create logic to create the instance
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    
    public ArmyObject armyObject;
    public GameObject BaseUnitPrefab;
    public GameObject mouseTail;


    void Start()
    {
        //Singleton
        instance = this;

        foreach (UnitObject unit in armyObject.unitList)
        {
            GameObject unitPrefab = Instantiate(BaseUnitPrefab, unit.spawnPosition, new Quaternion (0,0,0,0));
            unitPrefab.GetComponent<BaseUnitPrefab>().unitObject = unit;
        }

    }

}
