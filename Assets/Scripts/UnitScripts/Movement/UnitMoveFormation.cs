using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveFormation : MonoBehaviour
{
    //Formation Active Bool
    public bool formationActive;
    public List<GameObject> selectedUnitList;

    public UnitManager unitManager;

    private void Awake()
    {

    }

    private void Update()
    {
        selectedUnitList = GetComponent<UnitManager>().selectedUnitList;
    }

    //Find Formation Minimum Vector
    public Vector3 FindMinVector(List<GameObject> unitList)
    {
        Vector3 minimum = unitList[0].transform.position;
        foreach (GameObject unit in unitList)
        {
            if (unit.transform.position.x < minimum.x)
            {
                minimum.x = unit.transform.position.x;
            }
            if (unit.transform.position.y < minimum.y)
            {
                minimum.y = unit.transform.position.y;
            }
        }
        return minimum;
    }

    //Find Formation Maximum Vector
    public Vector3 FindMaxVector(List<GameObject> unitList)
    {
        Vector3 maximum = unitList[0].transform.position;
        foreach (GameObject unit in unitList)
        {
            if (unit.transform.position.x > maximum.x)
            {
                maximum.x = unit.transform.position.x;
            }
            if (unit.transform.position.y > maximum.y)
            {
                maximum.y = unit.transform.position.y;
            }
        }
        return maximum;
    }

    //Identify Formation Center Method
    public Vector3 FindFormationCenter(Vector3 minimum, Vector3 maximum)
    {
        Vector3 formationCenter;
        formationCenter.z = 0;
        formationCenter.x = (minimum.x + maximum.x) / 2;
        formationCenter.y = (minimum.y + maximum.y) / 2;
        return formationCenter;
    }

    public Vector3 FormationMoveCenter()
    {
        Vector3 min = FindMinVector(selectedUnitList);
        Vector3 max = FindMaxVector(selectedUnitList);
        Vector3 center;
        center.z = 0;
        if (selectedUnitList.Count > 1)
        {
            center = FindFormationCenter(min, max);
            formationActive = true;
        }
        else
        {
            center.x = 0;
            center.y = 0;
            formationActive = false;
        }
        return center;
    }

    //Identify Relative Offset based on Formation Center 
    public void FormationMove()
    {
        Vector3 offset;
        Vector3 formationCenter = FormationMoveCenter();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (formationActive == true)
        {

            foreach (GameObject unit in selectedUnitList)
            {
                offset = unit.transform.position - formationCenter;
                unit.GetComponent<MovePositionDirect>().SetMovePosition(mousePosition + offset); 

            }
        }
    }
}
