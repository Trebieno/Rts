using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _timeDestroy;
    

    private Unit _unit;
    private Enemy _enemy;
    private void Start() 
    {
        Destroy(gameObject, _timeDestroy);
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if (_unit && other.GetComponent<Unit>())
            return; 

        if (other.transform.GetComponent<IHealth>() != null)
        {
            IHealth health = other.transform.GetComponent<IHealth>();
            health.TakeDamage(_damage);
            Destroy(gameObject);

        }
    }

    public void Set(float damage, float timeDestroy, Unit unit = null)
    {
        _damage = damage;
        _timeDestroy = timeDestroy;
        _unit = unit;
    }
}

