using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Character
{  
    [SerializeField] private Transform _selectedSprite;
    private bool previousIsSelected = false;
    private bool _isSelected = false;

    private void Start()
    {
        UnitSelection.Instance.NpcView.Units.Add(this);

        if(_selectedSprite == null)
            _selectedSprite = transform.Find("Select");
    }

    public bool IsSelected => _isSelected;

    public void Select(bool state)
    {
        _isSelected = state;
        _selectedSprite.gameObject.SetActive(state);
    }
}
