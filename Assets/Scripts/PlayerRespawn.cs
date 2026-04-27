using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentCheckpoint;
    public PlayerHealth playerHealth;
    public PlayerMana playerMana;
    public GameObject bastet;

    void Start()
    {
        currentCheckpoint = transform.position; // Inicializa el checkpoint en la posición inicial del jugador
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint; // Actualiza el checkpoint a la nueva posición
    }

    public void Respawn()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Detiene cualquier movimiento residual
            rb.angularVelocity = 0f; // Detiene cualquier rotación residual
        }
        transform.position = currentCheckpoint; // Teletransporta al jugador a la posición del checkpoint
        bastet.transform.position = currentCheckpoint;

        if (playerHealth != null)
            playerHealth.RestoreHealth(); // Restaura la salud del jugador al máximo

        if (playerMana != null)
            playerMana.RestoreMana(); // Restaura el mana del jugador al máximo


        Debug.Log("Respawn en checkpoint");
    }
}