using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private int _waveNumber = 0;
    [SerializeField] private int _enemyCount = 0;
    [SerializeField] private List<GameObject> _spawns;
    [SerializeField] private bool _isWorking;

    public void StartWave()
    {
        _isWorking = true;
        _spawns = GameObject.FindGameObjectsWithTag("Spawn").ToList();
        for (int i = 0; i < _spawns.Count; i++)
        {
            SpawnEnemy spawn = _spawns[i].GetComponent<SpawnEnemy>();
            spawn.SetWaveSystem(GetComponent<WaveSystem>());
            StartCoroutine(spawn.Spawning());
        }
    }

    public void NextWave()
    {
        if(_enemyCount > 0) 
            return;

        _waveNumber++;
        _enemyCount = _waveNumber * 10;
        StartWave();
        SpawnEnemy spawn = _spawns[0].GetComponent<SpawnEnemy>();
        spawn.NpcLvlUp();
    }

    public void StopWave()
    {
        if (!_isWorking)
            return;
        _isWorking = false;
        _enemyCount = 0;
        _waveNumber--;
    }

    public int EnemyCountInfo()
    {
        return _enemyCount;
    }

    public bool WaveWorkingInfo()
    {
        return _isWorking;
    }

    public void SubtractionEnemyCount()
    {
        _enemyCount--;
    }
}
