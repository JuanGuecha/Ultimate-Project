using Unity.VisualScripting;
using UnityEngine;

public class TriggerMinPlat : MonoBehaviour
{
    // Referencia al objeto padre que tiene las imágenes
    public bool triggerActivated = false;
    [SerializeField] private GameObject doorFinal; // Referencia a la puerta final para activar su animación
    [SerializeField] private GameObject platformToShow; // Referencia a la plataforma que se mostrará al activar el trigger
    //[SerializeField] private GameObject gameManager; // Referencia al jugador para detectar su presencia
     // Variable para controlar si el jugador tiene las piezas necesarias


    void Start()
    {
        // Aseguramos que empiece oculto al iniciar el juego
        if (platformToShow != null)       platformToShow.SetActive(false);
        if (doorFinal != null)            doorFinal.SetActive(false);
        // if (gameManager == null)          Debug.LogError($"TriggerMinPlat: GameManager no asignado en {gameObject.name}");
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        //if (collider.CompareTag("Player") && gameManager.GetComponent<GameManager>().HasPieces)
        if(collider.CompareTag("Player") && triggerActivated)
        {
            if (platformToShow != null)
            {
                platformToShow.SetActive(true); // ¡El trigger le da "la chispa" de vida!
                doorFinal.SetActive(true); // Activamos la puerta final para que su animación se ejecute    
                Debug.Log("¡Plataformas activadas!");
            }
        }
    }
}