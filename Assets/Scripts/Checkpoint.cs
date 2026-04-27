using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerRespawn playerRespawn = collision.GetComponent<PlayerRespawn>();

        if (playerRespawn != null)
        {
            Vector3 newCheckpoint = respawnPoint != null  // Si se ha asignado un respawnPoint, úsalo; de lo contrario, 
            // usa la posición actual del checkpoint
                ? respawnPoint.position // Si se ha asignado un respawnPoint, úsalo
                : transform.position;// Si no se ha asignado un respawnPoint, usa la posición actual del checkpoint

            playerRespawn.SetCheckpoint(newCheckpoint);

            Debug.Log("Nuevo checkpoint guardado: " + newCheckpoint);
        }
    }
}