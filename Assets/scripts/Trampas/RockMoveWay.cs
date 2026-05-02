using UnityEngine;
using System.Collections;

public class RockMoveWay : MonoBehaviour
{
    [SerializeField] private Transform waypointParent;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stoppingDistance = 0.1f;
    [SerializeField] private float waitTimeAtEnd = 1f;
    TriggerRock triggerRock;

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;
    private bool isReturning = false;
    private bool isWaiting = false;

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
        if (isWaiting) return;

        Transform target = waypoints[currentWaypointIndex];

        // 🎯 Calcula dirección hacia el waypoint
        Vector2 direction = (target.position - transform.position).normalized;

        // ⚙️ Aplica velocidad (permite física + rotación)
        rb.linearVelocity = direction * speed;

        // 🎯 Detecta llegada
        if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        {
            if (!isReturning)
            {
                currentWaypointIndex++;

                // Al llegar al final del camino
                if (currentWaypointIndex >= waypoints.Length)
                {
                    rb.linearVelocity = Vector2.zero;
                    StartCoroutine(WaitAndReturn());
                }
            }
            else
            {
                currentWaypointIndex--;

                // Al regresar al primer waypoint
                if (currentWaypointIndex < 0)
                {
                    rb.linearVelocity = Vector2.zero;
                    Debug.Log("✅ Roca regresó al inicio");
                    enabled = false;
                }
            }
        }
    }

    IEnumerator WaitAndReturn()
    {
        isWaiting = true;
        Debug.Log($"⏳ Roca en el final. Esperando {waitTimeAtEnd} segundos...");
        yield return new WaitForSeconds(waitTimeAtEnd);

        isReturning = true;
        isWaiting = false;
        currentWaypointIndex = waypoints.Length - 2; // Apunta al penúltimo para empezar el regreso

        // Si solo hay un waypoint o algo raro, evitamos errores
        if (currentWaypointIndex < 0) currentWaypointIndex = 0;

        Debug.Log("🔄 Iniciando regreso al primer waypoint");
    }
}