using UnityEngine;

public class Shooting : Weapon
{
    protected override void RotatinonObjectOrMove()
    {
        transform.LookAt(new Vector3(_target.position.x, 0.5f, _target.position.z));
    }
}