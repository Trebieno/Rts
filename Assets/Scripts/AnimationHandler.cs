using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Character _character;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _character = GetComponent<Character>();

        if(_character.Movements == null) 
            Debug.Log(null);

        _character.Movements.Moved += CharacterOnMoved;
        _character.Damaged += CharacterOnDamaged;
    }

    private void OnDestroy() 
    {
        _character.Damaged -= CharacterOnDamaged;
        _animator.SetTrigger("Death");
    }

    private void CharacterOnDamaged()
    {
        _animator.SetTrigger("TakeDamage");
    }

    private void CharacterOnMoved()
    {
        _animator.SetBool("Run", true);
    }

    private void FixedUpdate() 
    {
        if(!_character.Movements.Agent.hasPath)
        {
            _animator.SetBool("Run", false);
        }
    }
}
