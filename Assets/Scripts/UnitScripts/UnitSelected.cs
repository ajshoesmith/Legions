using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//In Use? True

public class UnitSelected : MonoBehaviour
{
    public UnitManager unitManager;
    public static bool selectionModeActive = false;
    

    //Used for the Selection Rectangle
    private GameObject selectedGameObject;
    public bool selectedActive = false;


    //Used for Mobile Framework
    //private bool activationTimerActive = false;
    //private static float activationStartTime = 0;
    //private static float activationWaitTime = 1.5f;


    public void Awake()
    {
        unitManager = GameObject.Find("GameManager").GetComponent<UnitManager>();
        selectedGameObject = transform.Find("Selected").gameObject;
        SetSelectVisible(false);
    }

    void Update()
    {
        if (unitManager.selectedUnitList.Count == 0)
        {
            selectionModeActive = false;
        }
    }
    public void SetSelectVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }


    /*      For Press And Hold Selection (Mobile Framework)
    void OnMouseDown()
    {
        if (selectionModeActive == false)
        {
            activationStartTime = Time.time;
            activationTimerActive = true;
        }

        if (selectionModeActive == true && unitManager.selectedUnitList.Contains(gameObject) == true)
        {
            DeselectUnit();
        }

        else if (selectionModeActive == true && unitManager.selectedUnitList.Contains(gameObject) == false)
        {
            SelectUnit();
        }
    }

    void OnMouseDrag()
    {
        if (selectionModeActive == false && (Time.time - activationStartTime) > activationWaitTime && activationTimerActive == true)
        {
            selectionModeActive = true;
            SelectUnit();
        }
    }

    void OnMouseUpAsButton()
    {
        
    }

    void OnMouseUp()
    {
        activationStartTime = 0;
        activationTimerActive = false;
    }

    public void SetSelectVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    public void SelectUnit()
    {
        unitManager.selectedUnitList.Add(gameObject);
        SetSelectVisible(true);
        selectedActive = true;
    }

    public void DeselectUnit()
    {
        unitManager.selectedUnitList.Remove(gameObject);
        SetSelectVisible(false);
        selectedActive = false;
    }
    */
}
