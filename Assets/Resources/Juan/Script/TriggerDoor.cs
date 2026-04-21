
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public bool IsOpen=false; // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && gameObject.CompareTag("Sensor_1"))
        {
            Debug.Log("Puerta colisionó con el jugador, iniciando movimiento");
    
            IsOpen = true;      
        }
        //Para mas tags, se pueden agregar mas condiciones if con el mismo formato, cambiando el tag del sensor y el mensaje de depuración para cada caso.

    }
      
}
