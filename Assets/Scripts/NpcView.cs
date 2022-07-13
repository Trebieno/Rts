using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class NpcView : MonoBehaviour
{
    
    [SerializeField] private List<Character> _characters = new List<Character>();

    public void AddList(Character character) => _characters.Add(character);

    //public void OnDestroy(Character character) => _characters.Remove(character);

    public Character SearchingNearest(Character character)
    {
        List<Character> _otherObjects = _characters.FindAll(x => x.Command != character.Command);
        List<Character> _nearestObjects = _otherObjects.Where(x => Vector3.Distance(character.transform.position, x.transform.position) < 8f).ToList();
        _nearestObjects = _nearestObjects.OrderBy(x => Vector3.Distance(character.transform.position, x.transform.position)).ToList();
        return _nearestObjects?[0];
    }
}
