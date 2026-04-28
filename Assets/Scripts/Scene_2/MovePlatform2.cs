using UnityEngine;

public class MovePlatform2 : MonoBehaviour
{
    [SerializeField] private Transform waypointParent;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stoppingDistance = 0.1f;
    public bool ExitPlayer = false;
    [SerializeField] TriggerPlatform triggerPlatform;



    private Transform[] waypoints;



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
        if (waypoints.Length < 2) return; // Necesitamos al menos 2 puntos (Inicio y Fin)

        // 💡 DETERMINAMOS EL DESTINO
        // Si el jugador está: el destino es el último waypoint (índice 1 o final)
        // Si el jugador NO está: el destino es el primer waypoint (índice 0)
        int targetIndex = triggerPlatform.PlatformTriggered ? waypoints.Length - 1 : 0;

        // 🚀 MOVIMIENTO CONSTANTE
        // La plataforma siempre intenta ir hacia el targetIndex actual
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[targetIndex].position,
            speed * Time.fixedDeltaTime
        );
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

}