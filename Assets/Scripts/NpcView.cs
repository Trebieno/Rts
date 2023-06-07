using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;



public class NpcView : MonoBehaviour
{
    [SerializeField] private List<Unit>  _units;

    public List<Unit> Units => _units;

    // public Character SearchingNearest(Character character, float radius)
    // {
    //     _characters.RemoveAll(x => x == null);
    //     List<Character> _otherObjects = _characters.FindAll(x => x.Team != character.Team);
    //     List<Character> _nearestObjects = _otherObjects.Where(x => Vector3.Distance(character.transform.position, x.transform.position) < radius).ToList();
    //     _nearestObjects = _nearestObjects.OrderBy(x => Vector3.Distance(character.transform.position, x.transform.position)).ToList();
        
    //     if(_nearestObjects.Count > 0)
    //         return _nearestObjects[0];
    //     else
    //         return null;
    // }
}
