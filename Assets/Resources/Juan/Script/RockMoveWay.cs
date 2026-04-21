using UnityEngine;

public class RockMoveWay : MonoBehaviour
{
    [SerializeField] private Transform waypointParent;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stoppingDistance = 0.1f;
    TriggerRock triggerRock;
    
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;

    void Start()
    {
        triggerRock = GameObject.Find("TriggerRock").GetComponent<TriggerRock>();   
        if (waypointParent == null)
        {
            Debug.LogError("RockMoveWay: waypointParent no asignado");
            enabled = false;
            return;
        }

        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("RockMoveWay: Rigidbody2D no encontrado");
            enabled = false;
        }
    }


    void FixedUpdate()
    {
        
        if (waypoints.Length == 0) return;

        if (triggerRock.rockTriggered)
        {
            MoveToWaypoints();
        }
           
    }

    void MoveToWaypoints()
    {
        Transform target = waypoints[currentWaypointIndex];
        
        // 🎯 Calcula dirección hacia el waypoint
        Vector2 direction = (target.position - transform.position).normalized;
        
        // ⚙️ Aplica velocidad (permite física + rotación)
        rb.linearVelocity = direction * speed;

        // 🎯 Detecta llegada
        if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        {
            currentWaypointIndex++;

            // Se detiene en el último waypoint
            if (currentWaypointIndex >= waypoints.Length)
            {
                rb.linearVelocity = Vector2.zero;
                Debug.Log("✅ Roca llegó al waypoint final");
                enabled = false; // Desactiva el script
            }
        }
    }
}