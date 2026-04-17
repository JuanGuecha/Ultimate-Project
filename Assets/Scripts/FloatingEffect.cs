using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float height = 0.5f;

    private Vector3 initialPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position; //Guardamos 
        // la posición inicial del objeto para usarla como 
        // referencia en el movimiento de flotación
    }

    // Update is called once per frame
    void Update()
    {
        float newY = initialPosition.y + Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
         // Calculamos la nueva posición Y utilizando una función seno para crear un movimiento de flotación suave
         // y actualizamos la posición del objeto en cada frame
    }
}
