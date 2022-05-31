using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _timeDestroy;
    private float _damage;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }


    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, 0.22f, _mask))
        {
            if (hit.transform.GetComponent<IHealth>() != null)
            {
                //Debug.Log("Попал в " + hit.transform.name);
                var health = hit.transform.GetComponent<IHealth>();
                health.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }

    public void SetDamageBullet(float damage)
    {
        _damage = damage;
    }

    public void SetTimeDestroy(float timeDestroy)
    {
        _timeDestroy = timeDestroy;
    }

    public IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_timeDestroy);
        Destroy(gameObject);
    }
}
