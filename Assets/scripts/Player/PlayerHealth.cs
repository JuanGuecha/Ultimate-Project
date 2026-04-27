using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
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

        if (currentLives < 0) // Asegura que las vidas no sean negativas
            currentLives = 0; 

        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerDamage);
        healthUI.UpdateHealth(currentLives);

        if (currentLives > 0)
        {
            StartCoroutine(DamageCooldown()); // Inicia el cooldown de daño para evitar recibir daño inmediatamente después
        }
        else if (currentLives <= 0)
        {
            Die(); // Llama al método de muerte si las vidas llegan a 0 o menos
        }
    }

    public void Die()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerDeath);
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
        yield return new WaitForSeconds(damageCooldown); // Espera el tiempo de cooldown
        canTakeDamage = true;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            Debug.Log("Recibio daño ");
            TakeDamage(1);
        }
    }

    public void RestoreHealth()
    {
        currentLives = maxLives;

        Debug.Log("Vida restaurada a: " + currentLives);

        if (healthUI != null)
        {
            healthUI.UpdateHealth(currentLives);
        }
        else
        {
            Debug.LogError("healthUI no está asignado en PlayerHealth");
        }
    }
}