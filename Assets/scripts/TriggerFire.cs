using UnityEngine;

public class TriggerFire : MonoBehaviour
{
    [SerializeField] private bool _isActive; // Cambiamos el nombre a _isActive por convención de campos privados

// ✅ Propiedad pública: permite leer (get) pero no modificar desde fuera
    public bool IsActive => _isActive; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Jugador colisionó con el sensor de fuego, activando fuego");
            _isActive = true; // Modificamos la variable privada interna
        }
    }
}
