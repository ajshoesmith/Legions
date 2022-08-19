using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<GameObject> selectedUnitList;

    //Formation Active Bool
    public bool formationActive;

    //Quick Formation fields
    public float rightMouseStopwatch;
    public bool quickLineFormationActive;
    //float mouseStartTime = 0f;
    //float mouseHoldTime = 4f;

    void Awake()
    {
        selectedUnitList = new List<GameObject>();
    }

    void Update()
    {
        //Formation Move
        FormationMove();

        //Single Unit Move
        SingleUnitMovement();


    }
    
    private void FormationMove()
    {
        //Formation Move
        if (Input.GetMouseButtonUp(1) && selectedUnitList.Count != 0 && quickLineFormationActive != true)
        {
            GetComponent<UnitMoveFormation>().FormationMove();
        }
    }

    private void SingleUnitMovement()
    {
        //Single Unit Movement
        if (Input.GetMouseButtonDown(1) && selectedUnitList.Count == 1)
        {
            //StopWatch ends
            GetComponent<UnitMoveQuickLine>().rightMouseStopwatch = 0f;
            foreach (GameObject unit in selectedUnitList)
            {
                unit.GetComponent<MovePositionDirect>().SetMovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
}
        }
    
    }
}


    //if (rightMouseStopwatch >= 0.5 && selectedUnitList.Count > 1 && Mathf.Sqrt(Mathf.Pow(initialRightClick.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 2) +
    //Mathf.Pow(initialRightClick.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 2)) >= quickLineFormationGridThreshold)