using UnityEngine;

public class EnemyMove_Bat : MonoBehaviour
{
    [SerializeField] private Transform waypointParent;
    [SerializeField] private float speed = 2f;
    Animator animator;
    private Transform[] waypoints;
    Rigidbody2D rb;
    int currentWaypointIndex = 0;
    private SpriteRenderer spriteRenderer;
    public bool death=false;
    void Start()
    {
        if (waypointParent == null)
        {
            Debug.LogError("EnemyMove_Bat: waypointParent no asignado");
            enabled = false;
            return;
        }

        // Inicializar el array de waypoints con los hijos de waypointParent
        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
        // Obtener componentes necesarios
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("EnemyMove_Bat: Animator no encontrado");
            enabled = false;
        }
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("EnemyMove_Bat: Rigidbody2D no encontrado");
            enabled = false;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)       {
            Debug.LogError("EnemyMove_Bat: SpriteRenderer no encontrado");
            enabled = false;
        }
    }

    // Update is called once per frame
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
        rb.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime);
        
        // 🎯 Detección de llegada
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
 
}
