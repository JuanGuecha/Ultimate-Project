using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private Transform WayPointsParent;
    private GameObject[] activeEnemy;
    
    [SerializeField] private float checkInterval = 2f; 
    [SerializeField] private float minSpawnDelay = 0.2f; 
    [SerializeField] private float maxSpawnDelay = 0.8f; 

    private Transform[] waypoints;
    private Camera mainCamera; // Cache de la cámara para optimizar
    public int NumEnemy;

    void Start()
    {
        mainCamera = Camera.main; // Guardamos la cámara al inicio

        if (WayPointsParent == null)
        {
            Debug.LogError("SpawnManager: WayPointsParent no asignado");
            enabled = false;
            return;
        }

        if (enemyPrefab == null || enemyPrefab.Length == 0)
        {
            Debug.LogError("SpawnManager: enemyPrefab no asignado o vacío");
            enabled = false;
            return;
        }

        waypoints = new Transform[WayPointsParent.childCount];
        for (int i = 0; i < WayPointsParent.childCount; i++)
        {
            waypoints[i] = WayPointsParent.GetChild(i);
        }

        activeEnemy = new GameObject[waypoints.Length];
        StartCoroutine(SpawnEnemyAtWaypointWithDelay());
    }

    IEnumerator SpawnEnemyAtWaypointWithDelay()
    {
        while (true)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                // Primera condición: ¿El slot está vacío?
                // Segunda condición: ¿El punto de origen está fuera de cámara?
                if (activeEnemy[i] == null && !IsWaypointVisible(waypoints[i]))
                {
                    float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
                    yield return new WaitForSeconds(delay);

                    // Re-verificamos: Por si el jugador se movió hacia el waypoint durante el delay
                    if (activeEnemy[i] == null && !IsWaypointVisible(waypoints[i]))
                    {
                        Vector2 spawnPosition = waypoints[i].position;
                        GameObject prefabToSpawn = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
                        activeEnemy[i] = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

                        NumEnemy++;
                        Debug.Log($"Enemigo Spawned fuera de cámara en waypoint: {i}");
                    }
                }
            }
            yield return new WaitForSeconds(checkInterval);
        }
    }

    /// <summary>
    /// Comprueba si un Transform es visible por la cámara principal.
    /// </summary>
    private bool IsWaypointVisible(Transform wp)
    {
        if (mainCamera == null) mainCamera = Camera.main;
        
        Vector3 viewPos = mainCamera.WorldToViewportPoint(wp.position);
        
        // Si X o Y están entre 0 y 1, y Z es positivo, el objeto está en pantalla.
        return viewPos.x >= 0f && viewPos.x <= 1f && viewPos.y >= 0f && viewPos.y <= 1f && viewPos.z > 0;
    }
}