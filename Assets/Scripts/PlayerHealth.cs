using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxLives = 3;
    private int currentLives;
    public HealthUI healthUI;
    public int damageEnemy = 1; // Cantidad de daño que el jugador recibe al colisionar con un enemigo
    private bool canTakeDamage = true;
    public float damageCooldown = 1f;

    void Start()
    {
        currentLives = maxLives;
        healthUI.UpdateHealth(currentLives);
    }

    public void TakeDamage(int amount, Vector2 hitDirection)
    {
        if (!canTakeDamage) return;

        currentLives -= amount;
        Debug.Log($"Player took damage. Current lives: {currentLives}");
        gameObject.GetComponent<Playercontroller>().ApplyKnockback(hitDirection); // Aplica el knockback al jugador

        if (currentLives < 0)
            currentLives = 0;

        healthUI.UpdateHealth(currentLives);

        if (currentLives > 0)
        {
            StartCoroutine(DamageCooldown());
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        Debug.Log("Player can take damage again.");
        canTakeDamage = true;
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }
    */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Player collided with an enemy, taking damage.");
            Vector2 hitDirection = (collision.transform.position - transform.position).normalized; 

            TakeDamage(damageEnemy, hitDirection);

        }
    }
}