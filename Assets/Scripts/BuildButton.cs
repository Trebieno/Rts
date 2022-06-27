using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _hologram;
    [SerializeField] private GameObject _building;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _dist = 10f;

    private bool _isPlanning;
    private Camera _cam;
    private GameObject _newHologram;
    private Ray _mousePosToRay;

    private void Awake()
    {
        _cam = Camera.main;
        _newHologram = null;
    }

    private void Update()
    {
        _mousePosToRay = _cam.ScreenPointToRay(Input.mousePosition);

        if (_isPlanning && Input.GetMouseButtonDown(0) && _newHologram == null)
        {
            _newHologram = Instantiate(_hologram, Vector3.zero, Quaternion.identity);
        }

        if (_newHologram != null)
        {
            if (_isPlanning)
            {
                RaycastHit hit;
                if (Physics.Raycast(_mousePosToRay, out hit, Mathf.Infinity, _ground))
                {
                    _newHologram.transform.position = hit.point;
                }
                else
                {
                    _newHologram.transform.position = _mousePosToRay.origin + (_mousePosToRay.direction * _dist);
                }
            }

            if (Input.GetMouseButtonUp(0) &&
                _newHologram != null)
            {
                _isPlanning = false;
                Vector3 pos = _newHologram.transform.position;
                Destroy(_newHologram);
                Instantiate(_building, pos, Quaternion.identity);
                UnitSelection.canDrag = true;
            }
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UnitSelection.canDrag = false;
        _isPlanning = true;
    }
}
