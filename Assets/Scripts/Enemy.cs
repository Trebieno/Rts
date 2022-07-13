using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private void Awake()
    {
        Command = Commands.enemy;
    }
}
