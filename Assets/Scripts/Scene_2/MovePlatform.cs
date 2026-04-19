using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private Transform waypointParent;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stoppingDistance = 0.1f;
    
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;


    void Start()
    {
        if (waypointParent == null)
        {
            Debug.LogError("waypointParent no asignado");
            enabled = false;
            return;
        }

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
        if(Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < stoppingDistance)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Reinicia al primer waypoint
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.fixedDeltaTime);
    }

    private void OnColliderEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }   
    }

    private void OnColliderExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }   
    }
}