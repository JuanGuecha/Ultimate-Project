using UnityEngine;

public class TrampSpikes : MonoBehaviour
{
    Playercontroller playerController;
    public bool DiePlayer;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Playercontroller>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Aquí puedes agregar lógica adicional, como reproducir un sonido o una animación
            Debug.Log("¡Jugador ha sido atrapado por las trampas de pinchos!");
            // Por ejemplo, podrías llamar a un método en el controlador del jugador para manejar la muerte
            //playerController.Die();
                DiePlayer = true;
        }
    }
}
