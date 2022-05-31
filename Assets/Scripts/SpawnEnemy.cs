using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> _npcs;
    [SerializeField] private float _timeSpawn = 1f;    
    private WaveSystem _waveSystem;
    private GameObject _enemy;    

    public IEnumerator Spawning()
    {
        if(_waveSystem.EnemyCountInfo() > 0 && _waveSystem.WaveWorkingInfo())
        {
            RandomEnemy();
            yield return new WaitForSeconds(_timeSpawn);
            _waveSystem.SubtractionEnemyCount();
            Instantiate(_enemy, transform.position, transform.rotation);
            if(_waveSystem.EnemyCountInfo() - 1 >= 0)
                StartCoroutine(Spawning());
        }
    }

    private void RandomEnemy()
    {
        _enemy = _npcs[Random.Range(0, _npcs.Count)];
        // int rnd = Random.Range(0, 100);
        // if (rnd <= 30)
        //     _enemy = npcs[1];
        // else if (rnd >= 70)
        //     _enemy = npcs[0];
    }

    public void SetWaveSystem(WaveSystem waveSystem)
    {
        _waveSystem = waveSystem;
    }

    public void NpcLvlUp()
    {
        _timeSpawn -= 0.04f;
        for (int i = 0; i < _npcs.Count; i++)
        {
            _npcs[i].GetComponent<Enemy>().LvlUp();
        }
    }
}
