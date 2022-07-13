using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    private bool _isFire;
    private bool _isReload;
    private Character _character;

    [SerializeField] private int _curBullets = 30; 
    [SerializeField] private int _maxBullets = 30;

    [SerializeField] private float _bulletForce = 35;
    [SerializeField] private float _delya = 0.3f;
    [SerializeField] private float _reload = 1.5f;
    [SerializeField] private float _bulletTimeDestroy = 2;     
    [SerializeField] protected float _damage = 30;
    
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] protected Transform _firePoint;
    [SerializeField] private LayerMask _maskAttack;
    


    protected void Shoot()
    {
        if (_curBullets > 0 && _hit > 0)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _firePoint.transform.position, _firePoint.rotation);
            bullet.SetSettings(_damage, _bulletTimeDestroy, _character.Command);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            rb.AddForce(-bullet.transform.right * _bulletForce, ForceMode.Impulse);
            
            _curBullets--;
        }

        else if (_curBullets <= 0)
        {
            StartCoroutine(Reload());
        }

        StartCoroutine(Delay());
        return;
    }

    private IEnumerator Reload()
    {
        _isReload = true;
        yield return new WaitForSeconds(_reload);
        _curBullets = _maxBullets;
        _isReload = false;
    }

    private int _hit = 1;
    private IEnumerator Delay()
    {   
        yield return new WaitForSeconds(_delya);
        _hit++;
    }
}
