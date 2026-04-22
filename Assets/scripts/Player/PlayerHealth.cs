using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public HealthUI healthUI;

    private bool canTakeDamage = true;
    public float damageCooldown = 1f;

    void Start()
    {
        currentLives = maxLives;
        healthUI.UpdateHealth(currentLives);
    }

    public void TakeDamage(int amount)
    {
        if (!canTakeDamage) return;

        currentLives -= amount;

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
}