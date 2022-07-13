using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static List<Unit> units = new List<Unit>();
    public static bool canDrag = true;
    [SerializeField] private LayerMask _clickableMask;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private GameObject selectArea;
    [SerializeField] private GameObject targetForUnits;

    public bool drag = false;
    private Vector3 previousMousePos;
    private Camera cam;
    private Vector3 dragStartPos;
    private Rect selectionRect;

    

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.mousePosition != previousMousePos &&
            Input.GetMouseButton(0) && 
            !drag)
        {
            SetDrag(true);
        }
        previousMousePos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            if (drag)
            {
                SetDrag(false);
            }
        }

        if(Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _clickableMask))
                {
                    var unit = hit.collider.GetComponent<Unit>();
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        ClearSelection();
                        targetForUnits.SetActive(false);
                    }
                    if (unit != null)
                    {
                        unit.SquadSelect(true);
                    }
                    
                }
                else if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _groundMask))
                {
                    List<Vector3> targetPositionList = GetPositionListAround(hit.point, new float[] { 2f, 2*2, 2*3}, new int[] {5, 10, 20});

                    int targetPositionListIndex = 0;
                    foreach (Unit unit in units.FindAll(i => i.IsSelected))
                    {
                        targetForUnits.transform.position = new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z);
                        targetForUnits.SetActive(true);
                        //unit.Movements.SetTarget(targetPositionList[targetPositionListIndex]);
                        targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
                    }
                }

                else if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _enemyMask))
                {
                    var unit = hit.collider.GetComponent<Unit>();
                    //unit.Attacker. ();
                }
        }
        
        if (drag)
        {
            targetForUnits.SetActive(false);
            RectTransform area = selectArea.GetComponent<RectTransform>();
            DrawSelectArea(area);
            CalculateSelectionRect();

            units.RemoveAll(x => x == null);
            foreach (Unit unit in units)
            {
                unit.SquadSelect(selectionRect.Contains(cam.WorldToScreenPoint(unit.transform.position)));
            }
        }
    }

    private void DrawSelectArea(RectTransform trans)
    {
        Vector3 selectCenter = (Input.mousePosition + dragStartPos) / 2;
        trans.position = selectCenter;

        Vector3 deltaPos = Input.mousePosition - dragStartPos;
        Vector2 size = new Vector2(Mathf.Abs(deltaPos.x), Mathf.Abs(deltaPos.y));
        trans.sizeDelta = size;
    }

    private void CalculateSelectionRect()
    {
        selectionRect.xMin = dragStartPos.x > Input.mousePosition.x ? Input.mousePosition.x : dragStartPos.x;
        selectionRect.xMax = dragStartPos.x < Input.mousePosition.x ? Input.mousePosition.x : dragStartPos.x;
        selectionRect.yMin = dragStartPos.y > Input.mousePosition.y ? Input.mousePosition.y : dragStartPos.y;
        selectionRect.yMax = dragStartPos.y < Input.mousePosition.y ? Input.mousePosition.y : dragStartPos.y;
    }

    private void SetDrag(bool _drag)
    {
        if (canDrag)
        {
            if (_drag)
            {
                dragStartPos = Input.mousePosition;
            }
            drag = _drag;
            selectArea.SetActive(_drag);
        }
    }

    public static void ClearSelection()
    {
        foreach (Unit unit in units)
        {
            unit.SetSelect(false);
        }
    }

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDisntanceArray, int[] ringPositionCountArray)
    {
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPosition);
        for (int i = 0; i < ringDisntanceArray.Length; i++)
        {
            positionList.AddRange(GetPositionListAround(startPosition, ringDisntanceArray[i], ringPositionCountArray[i]));
        }

        return positionList;
    }

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360 / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }

}
