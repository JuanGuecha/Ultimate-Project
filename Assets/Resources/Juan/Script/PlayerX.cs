using System.Collections;
using UnityEngine;

public class PlayerX : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public HealthUI healthUI;

    private PlayerRespawn playerRespawn;

    private bool canTakeDamage = true;
    public float damageCooldown = 1f;

    void Start()
    {
        currentLives = maxLives;
        healthUI.UpdateHealth(currentLives);
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    public void TakeDamage(int amount)
    {
        if (!canTakeDamage) return; // Si el jugador está en cooldown de daño, no recibe daño

        currentLives -= amount;
        Debug.Log("Player recibió daño, vidas restantes: " + currentLives);
        StartCoroutine(DamageCooldown()); // Inicia el cooldown de daño para evitar recibir daño inmediatamente después
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine()); // Inicia la corrutina de muerte
    }
    IEnumerator DieCoroutine()
    {

        
        Debug.Log("Player murió");
        yield return new WaitForSeconds(2f); // Espera 2 segundos antes de respawnear
        // 🔥 Respawn en checkpoint
        if (playerRespawn != null)
        {
            playerRespawn.Respawn(); // Llama al método Respawn del PlayerRespawn
        }
        else
        {
            Debug.LogError("No hay PlayerRespawn en el Player");
        }
    }
    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        for ( int i = 0; i < damageCooldown; i++)
        {
            Debug.Log("Cooldown de daño: " + (damageCooldown - i) + " segundos restantes");
            yield return new WaitForSeconds(1f); // Espera 1 segundo antes de actualizar el cooldown
        }
        yield return new WaitForSeconds(damageCooldown); // Espera el tiempo de cooldown
        canTakeDamage = true;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            Debug.Log("Recibio daño ");
            TakeDamage(1);
            Destroy(collision.transform.parent.gameObject); // Destruye el objeto de ataque enemigo después de causar daño
        }
    }
}
