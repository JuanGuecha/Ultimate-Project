using System.Collections;
using UnityEngine;

public class MovingFire : MonoBehaviour
{
    [SerializeField] private Transform waypointParent;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stoppingDistance = 0.1f;

    
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;


    void Start()
    {
  

        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }

    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0) return; 
        MoveToWaypoints();
    }

    void MoveToWaypoints()
    {
        Transform target = waypoints[currentWaypointIndex];
        Vector2 direction = (target.position - transform.position).normalized;
        
        
        // ⚙️ Movimiento
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.fixedDeltaTime);
        // Paradas intermedias para activar animaciones o efectos
        //StartCoroutine(Waypoint());

        //🎯 Detección de llegada
        if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        
    }
    /*
    IEnumerator Waypoint()
    {
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < stoppingDistance)
        {
            // Aquí puedes activar animaciones o efectos específicos para cada waypoint
            Debug.Log($"Reached waypoint {currentWaypointIndex}");
            yield return new WaitForSeconds(1f); // Espera 1 segundo antes de continuar al siguiente waypoint
        }
    }
    */
}