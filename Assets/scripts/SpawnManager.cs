using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private Transform WayPointsParent;

    [SerializeField] private float minSpawnDelay = 0.2f; // Intervalo de tiempo entre spawns
    [SerializeField] private float maxSpawnDelay = 0.8f; // Retardo antes de iniciar el spawn
    private Transform[] waypoints;
    public int NumEnemy;
    void Start()
    {
        
        // Verificar que WayPointsParent no esté vacío o nulo
        if (WayPointsParent == null)
        {
            Debug.LogError("SpawnManager: WayPointsParent no asignado(Puntos de Spawn de enemigo)");
            enabled = false;
            return;
        }
        // Verificar que enemyPrefab no esté vacío o nulo
        if (enemyPrefab == null || enemyPrefab.Length == 0)
        {
            Debug.LogError("SpawnManager: enemyPrefab no asignado o vacío");
            enabled = false;
            return;
        }else
        {   // Inicializar el array de waypoints con los hijos de WayPointsParent
            waypoints = new Transform[WayPointsParent.childCount];
            for (int i = 0; i < WayPointsParent.childCount; i++)
            {
                waypoints[i] = WayPointsParent.GetChild(i);
            }
        }
        StartCoroutine(SpawnEnemyAtWaypointWithDelay());
    }
    IEnumerator SpawnEnemyAtWaypointWithDelay()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            Vector2 spawnPosition = waypoints[i].position;
            GameObject prefabToSpawn = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            NumEnemy++;
            Debug.Log("Enemigo " + NumEnemy + " Spawned en waypoint: " + i + " (delay: " + delay + "s)");
        }
    }

 
}
