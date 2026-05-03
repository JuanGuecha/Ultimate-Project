using UnityEngine;

public class MoveRockWay : MonoBehaviour
{
    [SerializeField] private Transform waypointParent;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stoppingDistance = 0.1f;

    RockRotation RotationScript;

    Transform rotacion;

    [SerializeField] private GameObject triggerRockObject;
    private SpriteRenderer spriteRenderer;

    private TriggerRock _triggerRock; // Cache del componente TriggerRock

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
        RotationScript = GetComponent<RockRotation>();
        if (RotationScript == null)
        {            Debug.LogError("MoveRockWay: RockRotation script no encontrado en el mismo GameObject.");
            enabled = false;
        }
            // Cacheamos el trigger para optimizar (Senior Practice 💡)
        if (triggerRockObject != null) {
            _triggerRock = triggerRockObject.GetComponent<TriggerRock>();
        }
    }

    void FixedUpdate() {
        if (waypoints.Length == 0 || _triggerRock == null) return;

        // Solo se mueve si el sensor está activado
        if (_triggerRock.rockTriggered) {
            MoveToWaypoints();
        }
    }

    void MoveToWaypoints()
    {
        
        Transform target = waypoints[currentWaypointIndex];
        Vector2 direction = (target.position - transform.position).normalized;
        
        // 🎨 Voltea el sprite según dirección en X
        if (direction.x < 0) {
            spriteRenderer.flipX = true; //
            RotationScript.rotationDirection = 1f; // Rotación antihoraria

        } else {
            spriteRenderer.flipX = false; //
            RotationScript.rotationDirection = -1f; // Rotación horaria
        }
        
        // ⚙️ Movimiento
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.fixedDeltaTime);
        
        // 🎯 Detección de llegada
        if (Vector2.Distance(transform.position, target.position) < stoppingDistance) {
            // ¿Es este el último waypoint de la lista?
            if (currentWaypointIndex == waypoints.Length - 1) {
                // ✅ HEMOS LLEGADO AL FINAL
                currentWaypointIndex = 0;           // La siguiente vez empezará desde el inicio
                _triggerRock.rockTriggered = false; // "Apagamos" el movimiento hasta nuevo aviso
                
                //Opcional: Podrías teletransportar la piedra al inicio si es una trampa de caída
                transform.position = waypoints[0].position; 
            } else {
                // Seguimos al siguiente punto
                currentWaypointIndex++;
            }
        }

    }

    private void OnDrawGizmos()
    {
    // 🔍 Solo dibujamos si hay un padre asignado y tiene hijos
    if (waypointParent == null || waypointParent.childCount == 0) return;

    Gizmos.color = Color.cyan; // Color de la ruta
    
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            Transform current = waypointParent.GetChild(i);
            
            // ⚪ Dibuja una esfera en cada punto
            Gizmos.DrawSphere(current.position, 0.2f);

            // 📏 Dibuja una línea hacia el siguiente punto
            if (i < waypointParent.childCount - 1)
            {
                Transform next = waypointParent.GetChild(i + 1);
                Gizmos.DrawLine(current.position, next.position);
            }
            else 
            {
                // 🔄 Línea de retorno al inicio (opcional, según tu lógica de loop)
                Transform first = waypointParent.GetChild(0);
                Gizmos.color = Color.red; // Rojo para indicar el retorno
                Gizmos.DrawLine(current.position, first.position);
            }
        }
    }
}
