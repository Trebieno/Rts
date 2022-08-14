using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Character
{  
    
    [SerializeField] private List<Unit> _squad;
    [SerializeField] private Transform _selectedSprite;
    private bool previousIsSelected = false;
    private bool _isSelected = false;

    private void Awake()
    {
        UnitSelection.Units.Add(this);
        _squad.Add(this);
        Command = Commands.unit;
        _selectedSprite = transform.Find("Select");
    }

    public List<Unit> AllSquadMembers() => _squad;

    public bool IsSelected => _isSelected;

    public void SetSelect(bool selected) => _isSelected = selected;

    public void SetActiveSprite(bool active) => _selectedSprite.gameObject.SetActive(active);

    public void SquadSelect(bool isSelected)
    {
        if (isSelected != previousIsSelected) 
        {     
            _squad.RemoveAll(x => x == null);
            for (int i = 0; i < _squad.Count; i++)
            {
                _squad[i].SetSelect(isSelected);
                _squad[i].SetActiveSprite(isSelected);
            }
        }
        previousIsSelected = isSelected;
    }
}
