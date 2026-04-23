using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerRespawn respawn = collision.GetComponent<PlayerRespawn>();

            if (respawn != null)
            {
                respawn.SetCheckpoint(transform.position); // Actualiza el checkpoint del jugador a la posición de este objeto
                Debug.Log("Checkpoint actualizado");
            }
        }
    }
}
