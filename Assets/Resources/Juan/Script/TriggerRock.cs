using JetBrains.Annotations;
using UnityEngine;

public class TriggerRock : MonoBehaviour
{
    
    public bool rockTriggered = false;
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Roca colisionó con el jugador, iniciando movimiento");
            rockTriggered = true;
        }
    }
}
