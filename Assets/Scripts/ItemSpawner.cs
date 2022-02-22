using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject checkpointPrefab;
    [SerializeField] int checkpointSpawnerDelay = 10;
    [SerializeField] float spawnRadius = 10;
    [SerializeField] GameObject[] powerUpPrefab;
    [SerializeField] int powerUpDelay= 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCheckpointRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
    IEnumerator SpawnCheckpointRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkpointSpawnerDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;

            Instantiate(checkpointPrefab,randomPosition,Quaternion.identity);
        }
        
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            int random = Random.RandomRange(0, powerUpPrefab.Length);
            Instantiate(powerUpPrefab[random], randomPosition, Quaternion.identity);
        }

    }
}
