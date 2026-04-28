using UnityEngine;

public class TriggerCameraShake : MonoBehaviour
{
    public bool isTrigger = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Jugador colisionó con el sensor de sacudida de cámara, iniciando sacudida");
    
            isTrigger = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Jugador salió del sensor de sacudida de cámara, deteniendo sacudida");
    
            isTrigger = false;
        }
    }
}
