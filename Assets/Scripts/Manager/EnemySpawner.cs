using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public float[] spawnRates;
    public GameObject[] enemis;
    public float maxDistance = 10f;
    public Transform player;

    private void Start()
    {
        Spawn(enemis[0]);
        for (int i = 0; i < enemis.Length; ++i)
        {
            StartCoroutine(SpawnCoroutine(enemis[i], spawnRates[i]));
        }
    }

    private IEnumerator SpawnCoroutine(GameObject enemy, float rate)
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);

            Spawn(enemy);
        }
    }

    private void Spawn(GameObject enemy)
    {
        Vector3 randomPoint = player.position + Random.onUnitSphere * Random.Range(0.5f, 1f) * maxDistance;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, maxDistance, NavMesh.AllAreas))
        {
            Instantiate(enemy, hit.position, Quaternion.identity);
        }
    }
}
