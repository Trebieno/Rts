using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static List<Unit> Units = new List<Unit>();
    public static bool СanDrag = true;
    
    [SerializeField] private LayerMask _clickableMask;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private GameObject _selectArea;
    [SerializeField] private GameObject _targetForUnits;

    private bool _drag = false;
    private Vector3 _previousMousePos;
    private Camera _cam;
    private Vector3 _dragStartPos;
    private Rect _selectionRect;

    

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Input.mousePosition != _previousMousePos && Input.GetMouseButton(0) && !_drag)
        {
            SetDrag(true);
        }
        _previousMousePos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            if (_drag)
            {
                SetDrag(false);
            }
        }

        if(Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _clickableMask))
            {
                var unit = hit.collider.GetComponent<Unit>();
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    ClearSelection();
                    _targetForUnits.SetActive(false);
                }
                if (unit != null)
                {
                    unit.SquadSelect(true);    
                } 
            }

            else if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _groundMask))
            {
                List<Vector3> targetPositionList = GetPositionListAround(hit.point, new float[] { 2f, 2*2, 2*3}, new int[] {5, 10, 20});

                int targetPositionListIndex = 0;
                foreach (Unit unit in Units.FindAll(i => i.IsSelected))
                {
                    _targetForUnits.transform.position = new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z);
                    _targetForUnits.SetActive(true);
                    unit.Movements.SetTarget(targetPositionList[targetPositionListIndex]);
                    targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
                    
                }
            }

            else if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _enemyMask))
            {
                var unit = hit.collider.GetComponent<Unit>();
                //unit.Attacker. ();
            }
        }
        
        if (_drag)
        {
            _targetForUnits.SetActive(false);
            RectTransform area = _selectArea.GetComponent<RectTransform>();
            DrawSelectArea(area);
            CalculateSelectionRect();

            Units.RemoveAll(x => x == null);
            foreach (Unit unit in Units)
            {
                unit.SquadSelect(_selectionRect.Contains(_cam.WorldToScreenPoint(unit.transform.position)));
            }
        }
    }

    private void DrawSelectArea(RectTransform trans)
    {
        Vector3 selectCenter = (Input.mousePosition + _dragStartPos) / 2;
        trans.position = selectCenter;

        Vector3 deltaPos = Input.mousePosition - _dragStartPos;
        Vector2 size = new Vector2(Mathf.Abs(deltaPos.x), Mathf.Abs(deltaPos.y));
        trans.sizeDelta = size;
    }

    private void CalculateSelectionRect()
    {
        _selectionRect.xMin = _dragStartPos.x > Input.mousePosition.x ? Input.mousePosition.x : _dragStartPos.x;
        _selectionRect.xMax = _dragStartPos.x < Input.mousePosition.x ? Input.mousePosition.x : _dragStartPos.x;
        _selectionRect.yMin = _dragStartPos.y > Input.mousePosition.y ? Input.mousePosition.y : _dragStartPos.y;
        _selectionRect.yMax = _dragStartPos.y < Input.mousePosition.y ? Input.mousePosition.y : _dragStartPos.y;
    }

    private void SetDrag(bool _drag)
    {
        if (СanDrag)
        {
            if (_drag)
            {
                _dragStartPos = Input.mousePosition;
            }
            this._drag = _drag;
            _selectArea.SetActive(_drag);
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

    public static void ClearSelection()
    {
        foreach (Unit unit in Units)
        {
            unit.SetSelect(false);
        }
    }

}
