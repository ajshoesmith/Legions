using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGroupManager : MonoBehaviour
{
    // Control Group Sutffs (Duh)
    public int controlGroupCount = 0;
    public List<GameObject> controlGroup1 = new List<GameObject>();
    public List<GameObject> controlGroup2 = new List<GameObject>();
    public List<GameObject> controlGroup3 = new List<GameObject>();
    public List<GameObject> controlGroup4 = new List<GameObject>();
    public List<GameObject> controlGroup5 = new List<GameObject>();
    private List<List<GameObject>> controlGroupList = new List<List<GameObject>>();
    public List<ControlGroupButton> controlButtonList = new List<ControlGroupButton>();

    public List<GameObject> selectedUnitList;
    public UnitManager unitManager;

    void Awake()
    {
        controlGroupList.Add(controlGroup1);
        controlGroupList.Add(controlGroup2);
        controlGroupList.Add(controlGroup3);
        controlGroupList.Add(controlGroup4);
        controlGroupList.Add(controlGroup5);
    }

    void Update()
    {
        selectedUnitList = GetComponent<UnitManager>().selectedUnitList;
    }

    //######## Control Group ########//

    //Method For the Add Control Group Button
    public void ControlGroupAddButton(bool triggerActive)
    {
        if (triggerActive == true)
        {
            if (selectedUnitList.Count > 0 && controlGroupCount < 5)
            {
                controlGroupCount = controlGroupCount + 1;
                controlGroupList[controlGroupCount - 1].AddRange(selectedUnitList);
                controlButtonList[controlGroupCount - 1].gameObject.SetActive(true);

                // Stuffs for if we want the Add Button to be on the right and to move as you add more control groups
                //GameObject buttonA = GameObject.Find("ControlGroupAddButton");
                //buttonA.transform.localPosition = new Vector3(buttonA.transform.localPosition.x + 150, buttonA.transform.localPosition.y, 0);
                //foreach (Unit unit in controlGroupList[controlGroupCount - 1]) { Debug.Log(unit); }
            }
        }
    }

    //Selects units in control group when button is clicked
    public void SelectControlGroup(int controlGroupNumber)
    {
        DeselectAllUnits();

        foreach (GameObject unit in controlGroupList[controlGroupNumber - 1])
        {
            SelectUnit(unit);
        }
    }

    //Select Unit Method
    void SelectUnit(GameObject unit)
    {
        unit.GetComponent<UnitSelected>().SetSelectVisible(true);
        selectedUnitList.Add(unit);
    }

    //Deselect All Units Method
    void DeselectAllUnits()
    {
        foreach (GameObject unit in selectedUnitList)
        {
            unit.GetComponent<UnitSelected>().SetSelectVisible(false);
        }
        selectedUnitList.Clear();
    }

}



