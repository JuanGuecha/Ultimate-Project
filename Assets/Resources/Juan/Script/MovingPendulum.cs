using UnityEngine;

/// <summary>
/// Controla el movimiento de un péndulo usando lógica cinemática y funciones matemáticas.
/// Basado en el Movimiento Armónico Simple (MAS).
/// </summary>
public class MovingPendulum : MonoBehaviour
{
    [Header("Configuración del Péndulo")]
    [Tooltip("Ángulo máximo de oscilación en grados")]
    [SerializeField] private float amplitude = 45f;
    
    [Tooltip("Velocidad de la oscilación (Frecuencia)")]
    [SerializeField] private float frequency = 2f;

    private Rigidbody2D rb;
    private float timer;

    private void Awake()
    {
        // ✅ Caching de componentes: Estándar del proyecto para optimizar rendimiento.
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError($"[MovingPendulum] Falta Rigidbody2D en {gameObject.name}");
            return;
        }

        // Forzamos el tipo Kinematic para tener control total por script sin interferencia de gravedad.
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void FixedUpdate()
    {
        SwingKinematic();
    }

    private void SwingKinematic()
    {
        /* 
         * 💡 MATEMÁTICA DEL PÉNDULO (Movimiento Armónico Simple):
         * 
         * 1. El Tiempo (_timer): 
         *    Multiplicamos el tiempo acumulado por la frecuencia. 
         *    Esto determina qué tan rápido avanzamos por la onda Senoidal.
         * 
         * 2. La Función Seno (Mathf.Sin):
         *    Sin(x) siempre devuelve un valor entre -1 y 1. 
         *    Esto crea el vaivén natural (oscilación) de forma infinita.
         * 
         * 3. La Amplitud (_amplitude):
         *    Al multiplicar el resultado del Seno (-1 a 1) por la amplitud (ej. 45),
         *    obtenemos un ángulo resultante que oscila entre -45° y 45°.
         * 
         * Fórmula Final: Ángulo = Amplitud * Sin(Tiempo * Frecuencia)
         */

        timer += Time.fixedDeltaTime * frequency;
        float angle = Mathf.Sin(timer) * amplitude;

        // ✅ MoveRotation: Aplica la rotación física calculada.
        // Al rotar el PADRE, el HIJO (que está desplazado en Y) describe el arco del péndulo.
        rb.MoveRotation(angle);
    }
}