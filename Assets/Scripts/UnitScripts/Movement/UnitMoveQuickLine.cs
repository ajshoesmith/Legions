using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveQuickLine : MonoBehaviour
{
    //Quick Formation fields
    public float rightMouseStopwatch = 0f;
    private Vector3 initialRightClick;
    public bool quickLineFormationActive;
    public List<Vector3> quickLinePosList;
    public List<GameObject> selectedUnitList;
    public UnitManager unitManager;
    private double quickLineFormationGridThreshold = 0.5;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        selectedUnitList = GetComponent<UnitManager>().selectedUnitList;

        //Begins stopwatch to determine Quick Line Formation
        if (Input.GetMouseButtonDown(1) && selectedUnitList.Count > 1)
        {
            initialRightClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            
        }

        if (Input.GetMouseButton(1) && selectedUnitList.Count > 1)
        {
            //Stopwatch begins
            rightMouseStopwatch += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(1) && quickLineFormationActive != true)
        {
            rightMouseStopwatch = 0f;
        }
        
            //Quick Line Formation

        if (rightMouseStopwatch >= 0.5 && selectedUnitList.Count > 1 && Mathf.Sqrt(Mathf.Pow(initialRightClick.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 2) +
            Mathf.Pow(initialRightClick.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 2)) >= quickLineFormationGridThreshold)
        {
            Debug.Log(quickLineFormationActive);
            GetComponent<UnitManager>().formationActive = false;
            quickLineFormationActive = true;
            QuickLineSpacingPreview(selectedUnitList, initialRightClick);
            if (Input.GetMouseButtonUp(1) && quickLineFormationActive == true)
            {
                //QuickLineSpacing(selectedUnitList, initialRightClick); < -- Probably Not Necessary (May Delete)
                foreach (GameObject unit in selectedUnitList)
                {
                    unit.GetComponent<UnitPreview>().SetPreviewVisible(false);
                }
                quickLineFormationActive = false;
                rightMouseStopwatch = 0f;
            }
        }
        
        
    }

    //######## Quick Line Formation Movement ########//
    public void QuickLineSpacing(List<GameObject> unitList, Vector3 initialVector)                // < ------  Merged into QuickLineSpacing Preview, may not be necessary (not currently utilized)
    {
        foreach (GameObject unit in unitList)
        {
            Vector3 quickLineMovePosition;
            quickLineMovePosition.z = 0;
            quickLineMovePosition.x = initialVector.x + (((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - initialVector.x) / (unitList.Count - 1)) * unitList.IndexOf(unit));
            quickLineMovePosition.y = initialVector.y + (((Camera.main.ScreenToWorldPoint(Input.mousePosition).y - initialVector.y) / (unitList.Count - 1)) * unitList.IndexOf(unit));
            unit.GetComponent<MovePositionDirect>().movePosition = quickLineMovePosition;
        }
    }
    
    public void QuickLineSpacingPreview(List<GameObject> unitList, Vector3 initialVector)                 //Secrets to formation move preview is here (specifically the angle management)
    {
        foreach (GameObject unit in unitList)
        {
            unit.GetComponent<UnitPreview>().SetPreviewVisible(true);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 quickLineMovePosition;
            quickLineMovePosition.z = 0;
            quickLineMovePosition.x = initialVector.x + (((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - initialVector.x) / (unitList.Count - 1)) * unitList.IndexOf(unit));
            quickLineMovePosition.y = initialVector.y + (((Camera.main.ScreenToWorldPoint(Input.mousePosition).y - initialVector.y) / (unitList.Count - 1)) * unitList.IndexOf(unit));
            unit.GetComponent<UnitPreview>().previewGameObject.transform.position = quickLineMovePosition;
            Vector3 direction = new Vector2((mousePosition.x - initialRightClick.x) / 2, (mousePosition.y - initialRightClick.y) / 2);
            unit.GetComponent<UnitPreview>().previewGameObject.transform.right = direction;
            if (Input.GetMouseButtonUp(1))
            {
                unit.GetComponent<MovePositionDirect>().finalRotation = unit.GetComponent<UnitPreview>().previewGameObject.transform.right;
                unit.GetComponent<MovePositionDirect>().movePosition = quickLineMovePosition;
            }
        }
    }
    

}
