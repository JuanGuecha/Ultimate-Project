using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval;
    public float spawnRadius;
    public GameObject spanwPoint;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }


    void Update()
    {

    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnInterval);
        Vector2 Enemyspawnpos = new Vector2(Random.Range(-spawnRadius, spawnRadius), 0.5f);
        Instantiate(enemyPrefab, Enemyspawnpos, Quaternion.identity);
        StartCoroutine(SpawnEnemy());
        //Logica para un spawnpoint fijo
        //Instantiate(enemyPrefab, spanwPoint.transform.position, Quaternion.identity);
    }
}