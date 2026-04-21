using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidad = 2f;
    public float distancia = 3f;

    private Vector2 puntoInicial;
    private int direccion = 1;

    void Start()
    {
        puntoInicial = transform.position;
    }

    void Update()
    {
        // Mover la plataforma en el eje X
        transform.Translate(Vector2.right * velocidad * direccion * Time.deltaTime);

        // Calcular distancia recorrida desde el punto inicial
        float distanciaRecorrida = Mathf.Abs(transform.position.x - puntoInicial.x);

        // Invertir dirección al llegar al límite
        if (distanciaRecorrida >= distancia)
        {
            direccion *= -1;
        }
    }
}