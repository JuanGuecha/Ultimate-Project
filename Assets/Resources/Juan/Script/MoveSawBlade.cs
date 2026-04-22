using UnityEngine;

public class MoveSawBlade : MonoBehaviour
{
    [SerializeField] private Transform waypointParent;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stoppingDistance = 0.1f;
    private SpriteRenderer spriteRenderer;

    
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (spriteRenderer == null)       {
            Debug.LogError("SawBlade: SpriteRenderer no encontrado");
            enabled = false;
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
        
        // 🎨 Voltea el sprite según dirección en X
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction.x < 0; // true si va izquierda
        }
        
        // ⚙️ Movimiento
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.fixedDeltaTime);
        
        // 🎯 Detección de llegada
        if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
