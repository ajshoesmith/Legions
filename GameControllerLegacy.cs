using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//######## Chronology of Features ########//
//In theory, features should be in this order (not always the case [fields, update(), Methods] )
/* Highlight Selection
 * Formation Movement
 * Quick Line Formation
 * Control Groups
 * Draw Path
 */

public class GameController : MonoBehaviour
{
    
    //Singleton
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            //create logic to create the instance
            if (instance == null)
            {
                GameObject go = new GameObject("GameController");
                go.AddComponent<GameController>();
            }
            return instance;
        }
    }

    [SerializeField] private Transform selectionAreaTransform;
    [SerializeField] private double quickLineFormationGridThreshold;

    //Highlight Selection
    private Vector3 startMousePos;
    private Vector3 releaseMousePos;
    public List<Unit> selectedUnitList;

    // Control Group Sutffs (Duh)
    public int controlGroupCount = 0;
    public List<Unit> controlGroup1 = new List<Unit>();
    public List<Unit> controlGroup2 = new List<Unit>();
    public List<Unit> controlGroup3 = new List<Unit>();
    public List<Unit> controlGroup4 = new List<Unit>();
    public List<Unit> controlGroup5 = new List<Unit>();
    private List<List<Unit>> controlGroupList = new List<List<Unit>>();
    public List<ControlGroupButton> controlButtonList = new List<ControlGroupButton>();

    //Formation Active Bool
    public bool formationActive;
    
    //Quick Formation fields
    public float rightMouseStopwatch;
    private Vector3 initialRightClick;
    public bool quickLineFormation;
    public List<Vector3> quickLinePosList;

    //Draw Path
    [SerializeField] private Line linePrefab;
    //Length of Line Points (the less, the cleaner the line looks)
    [SerializeField] public const float lineLength = 1f;
    private Line currentLine;
    public List<Vector2> drawPathVectors = new List<Vector2>();
    public bool drawPathActive = false;
    [SerializeField] private DrawPathPosPrefab drawPathPosPrefab;


    //Highlight Vars
    
    public bool highlightActive;
    float highlightTimer;
    float highlightHoldDur = 0.75f;
    


    private void Awake()
    {
        //Singleton
        instance = this;

        //Creates selectedUnitList
        selectedUnitList = new List<Unit>();
        selectionAreaTransform.gameObject.SetActive(false);

        //Adds Control Group lists to THE Control Group List
        controlGroupList.Add(controlGroup1);
        controlGroupList.Add(controlGroup2);
        controlGroupList.Add(controlGroup3);
        controlGroupList.Add(controlGroup4);
        controlGroupList.Add(controlGroup5);
    }

    // Update is called once per frame
    void Update()
    {
    
        //######## Controls ########//
        
        //HighlightMode WIP//
        /*
        if (Input.GetMouseButtonDown(0))
        //Left Mouse Button Pressed
        {
            highlightTimer = Time.time;
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectionAreaTransform.gameObject.SetActive(true);
        }
        else if(Input.GetMouseButton(0))
        {
            if(Time.time - highlightTimer > highlightHoldDur)
            {
                highlightActive = true;
            }
        }*/
        

        
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
            selectionAreaTransform.localScale = (upperRight - lowerLeft)*100;
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
                Unit unit = collider2D.GetComponent<Unit>();
                if (unit != null)
                {
                    SelectUnit(unit);
                    //unit.SetSelectVisible(true);
                    //selectedUnitList.Add(unit);
                }
            }

            //DrawPath 
            //drawPathActive = false;
        }

        //Begins stopwatch to determine Quick Line Formation
        if (Input.GetMouseButtonDown(1) && selectedUnitList.Count > 1)
        {
            initialRightClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           
            //Stopwatch begins
            GetComponent<GlobalStopwatch>().Begin();

        }

        //Determines if formation move is true (not really sure if this is necessary tbh)
        if (selectedUnitList.Count > 1)
        {
            formationActive = true;
        }

        //Formation Move
        if (Input.GetMouseButtonUp(1) && selectedUnitList.Count != 0 && quickLineFormation != true)
        {
            FormationMove();
            GetComponent<GlobalStopwatch>().Reset();
        }


        //Quick Line Formation

        rightMouseStopwatch = GetComponent<GlobalStopwatch>().GetMilliseconds();
        if (rightMouseStopwatch >= 0.5 && selectedUnitList.Count > 1 && Mathf.Sqrt(Mathf.Pow(initialRightClick.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 2) + 
            Mathf.Pow(initialRightClick.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 2)) >= quickLineFormationGridThreshold)
        {
            formationActive = false;
            quickLineFormation = true;
            QuickLineSpacingPreview(selectedUnitList, initialRightClick);
            if (Input.GetMouseButtonUp(1) && quickLineFormation == true)
            {
                //QuickLineSpacing(selectedUnitList, initialRightClick); < -- Probably Not Necessary (May Delete)
                foreach (Unit unit in selectedUnitList)
                {
                    unit.GetComponent<Unit>().SetPreviewVisible(false);
                }
                quickLineFormation = false;
                GetComponent<GlobalStopwatch>().Reset();
            }
        }

        //Single Unit Movement
        if (Input.GetMouseButtonUp(1) && selectedUnitList.Count == 1)
        {
            //StopWatch ends
            GetComponent<GlobalStopwatch>().Reset();
            foreach (Unit unit in selectedUnitList)
            {
                unit.GetComponent<MovePositionDirect>().SetMovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        //Draw Path
        //Mouse Postition relative to game world                                                                                // Fix All of the below
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.Space)) 
        if (selectedUnitList.Count != 0 && Input.GetMouseButtonDown(0))
        {
            foreach (Unit unit in selectedUnitList)
            {
                if (Physics2D.IsTouching(GameObject.Find("MouseTail").GetComponent<CircleCollider2D>(), unit.GetComponent<BoxCollider2D>()))
                {
                    //Debug.Log("True");
                    drawPathActive = true;
                    currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);
                    

                }
            }
            
        }
        if (Input.GetMouseButton(0) && GameObject.Find("Line(Clone)") != null && drawPathActive == true)
        {
            //Debug.Log(currentLine);
            currentLine.SetPosition(mousePos);
            if (linePrefab.GetComponent<Line>()._renderer.positionCount >= 2)                               // < ---- Probs not right
            { 
                Vector3 drawPathEuler = DrawPathVectorDegree(drawPathVectors[drawPathVectors.Count - 1], drawPathVectors[drawPathVectors.Count - 2]);
                //DrawPathPreview(drawPathEuler);
            }
        }
        if (drawPathActive == true && Input.GetMouseButton(0) != true)
        {
            DrawPathPreview(selectedUnitList, drawPathVectors);
        }

    }

    //######## Highlight Mode ########//

    //Select Unit Method
    public void SelectUnit(Unit unit)
    {
        unit.SetSelectVisible(true);
        selectedUnitList.Add(unit);
    }
    //Deselect All Units Method
    public void DeselectAllUnits()
    {
        foreach (Unit unit in selectedUnitList)
        {
            unit.SetSelectVisible(false);
        }
        selectedUnitList.Clear();
    }



    //######## Formation Movement ########//

    //Find Formation Minimum Vector
    public Vector3 FindMinVector(List<Unit> unitList)
    {
        Vector3 minimum = unitList[0].transform.position;
        foreach (Unit unit in unitList)
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
    public Vector3 FindMaxVector(List<Unit> unitList)
    {
        Vector3 maximum = unitList[0].transform.position;
        foreach (Unit unit in unitList)
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
    private void FormationMove()
    {
        Vector3 offset;
        Vector3 formationCenter = GameController.Instance.FormationMoveCenter();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (formationActive == true)
        {
            
            foreach (Unit unit in selectedUnitList)
            {
                offset = unit.transform.position - formationCenter;
                unit.GetComponent<MovePositionDirect>().SetMovePosition(mousePosition + offset);

            }
        }
    }


    //######## Quick Line Formation Movement ########//

    public void QuickLineSpacing(List<Unit> unitList, Vector3 initialVector)                // < ------  Merged into QuickLineSpacing Preview, may not be necessary (not currently utilized)
    {
        foreach (Unit unit in unitList)
        {
            Vector3 quickLineMovePosition;
            quickLineMovePosition.z = 0;
            quickLineMovePosition.x = initialVector.x + (((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - initialVector.x) / (unitList.Count - 1)) * unitList.IndexOf(unit));
            quickLineMovePosition.y = initialVector.y + (((Camera.main.ScreenToWorldPoint(Input.mousePosition).y - initialVector.y) / (unitList.Count - 1)) * unitList.IndexOf(unit));
            unit.GetComponent<MovePositionDirect>().movePosition = quickLineMovePosition;
        }
    }

    public void QuickLineSpacingPreview(List<Unit> unitList, Vector3 initialVector)                 //Secrets to formation move preview is here (specifically the angle management)
    {
        foreach (Unit unit in unitList)
        {
            unit.GetComponent<Unit>().SetPreviewVisible(true);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 quickLineMovePosition;
            quickLineMovePosition.z = 0;
            quickLineMovePosition.x = initialVector.x + (((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - initialVector.x) / (unitList.Count - 1)) * unitList.IndexOf(unit));
            quickLineMovePosition.y = initialVector.y + (((Camera.main.ScreenToWorldPoint(Input.mousePosition).y - initialVector.y) / (unitList.Count - 1)) * unitList.IndexOf(unit));
            unit.previewGameObject.transform.position = quickLineMovePosition;
            Vector3 direction = new Vector2((mousePosition.x - initialRightClick.x) / 2, (mousePosition.y - initialRightClick.y) / 2);
            unit.previewGameObject.transform.right = direction;
            if (Input.GetMouseButtonUp(1))
            {
                unit.GetComponent<MovePositionDirect>().finalRotation = unit.previewGameObject.transform.right;
                unit.GetComponent<MovePositionDirect>().movePosition = quickLineMovePosition;
            }
        }
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

        foreach (Unit unit in controlGroupList[controlGroupNumber - 1])
        {
            SelectUnit(unit);
        }
    }
    
    //######## Draw Path #########// WIP

    public void DrawPathAddVector(Vector2 pos)
    {
        drawPathVectors.Add(pos);
    }

    public Vector3 DrawPathVectorDegree(Vector2 currentVector, Vector2 previousVector)
    {
        //Calculates the angle of the last 2 line vectors for rotation
        float x = currentVector.x - previousVector.x;
        float y = currentVector.y - previousVector.y;
        float h = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        float radians = Mathf.Acos(x / h);
        float degree = radians * Mathf.Rad2Deg;
        if(y < 0)
        {
            degree = 360-degree;
        }
        Vector3 eulerRotation = new Vector3(0, 0, degree);
        return eulerRotation;
    }

    //if let go hold mouse(0) during drawpath                           Still need to find appropriate spacing relative to DrawPathPosPrefab parent
    public void DrawPathPreview(List<Unit> unitList, List<Vector2> pathList)                    // DrawPathPreview(Vector3 eulerVector, List<Unit> unitList, List<Vector2> pathList)     
    {
    } 
}
