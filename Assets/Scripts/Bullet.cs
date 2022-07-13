using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _timeDestroy;

    private Commands _command;
    
    private void Start() => Destroy(gameObject, _timeDestroy);
    
    private void OnTriggerEnter(Collider other) 
    {
        if(_command == other.GetComponent<Character>().Command) 
            return;

        if (other.TryGetComponent(out IHealth health)) 
            health.TakeDamage(_damage);

        Destroy(gameObject);
    }

    public void SetSettings(float damage, float timeDestroy, Commands command)
    {
        _damage = damage;
        _timeDestroy = timeDestroy;
        _command = command;
    }
}

