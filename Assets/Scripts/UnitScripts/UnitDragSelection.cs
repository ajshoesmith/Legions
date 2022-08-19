using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDragSelection : MonoBehaviour
{

    //Highlight Selection
    private Vector3 startMousePos;
    private Vector3 releaseMousePos;
    public List<GameObject> selectedUnitList;
    [SerializeField] private Transform selectionAreaTransform;

    // Start is called before the first frame update
    void Start()
    {
        selectionAreaTransform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Updates selectedUnitList
        selectedUnitList = GetComponent<UnitManager>().selectedUnitList;

        if (Input.GetMouseButtonDown(0))
        //Left Mouse Button Pressed
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectionAreaTransform.gameObject.SetActive(true);
        }

        //Visible Overlap Rectangle (Selection Area)

        if (Input.GetMouseButton(0))
        {
            //Left Mouse Button Held Down
            Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startMousePos.x, currentMousePos.x),
                Mathf.Min(startMousePos.y, currentMousePos.y)
                );
            Vector3 upperRight = new Vector3(
                Mathf.Max(startMousePos.x, currentMousePos.x),
                Mathf.Max(startMousePos.y, currentMousePos.y)
                );
            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = (upperRight - lowerLeft) * 100;
        }

        //Visible Overlap Rectangle (Selection Area) Cont'd
        if (Input.GetMouseButtonUp(0) && GetComponent<UITest>().IsPointerOverUIElement() == false)
        //Left Mouse Button Released
        {
            selectionAreaTransform.gameObject.SetActive(false);
            releaseMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startMousePos, releaseMousePos);

            //Deselect all units
            //foreach (Unit unit in selectedUnitList)
            //{
            //    unit.SetSelectVisible(false);
            //}
            DeselectAllUnits();

            //Select Units within Selection Area
            foreach (Collider2D collider2D in collider2DArray)
            {
                GameObject unit = collider2D.gameObject;
                if (unit != null)
                {
                    SelectUnit(unit);
                    //unit.SetSelectVisible(true);
                    //selectedUnitList.Add(unit);
                }
            }
        }
    }

    //######## Highlight Mode ########//

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
