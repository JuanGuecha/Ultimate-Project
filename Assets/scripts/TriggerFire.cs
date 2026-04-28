using UnityEngine;

public class TriggerFire : MonoBehaviour
{
    public bool IsActive = false;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Jugador colisionó con el sensor de fuego, activando fuego");
    
            IsActive = true;
        }
    }
}
