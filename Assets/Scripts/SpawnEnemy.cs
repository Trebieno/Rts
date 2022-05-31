using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> npcs;
    [SerializeField] private float timeSpawn = 1f;

    void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        GameObject enemy = Instantiate(npcs[Random.Range(0, npcs.Count - 1)], transform.position, transform.rotation);
        yield return new WaitForSeconds(timeSpawn);

        StartCoroutine(Spawning());
    }
}
