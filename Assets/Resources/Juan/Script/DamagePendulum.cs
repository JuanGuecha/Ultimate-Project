using UnityEngine;

public class DamagePendulum : MonoBehaviour
{
    public int damageAmount;
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized; 
            if(collision.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(damageAmount, direction);
            }
        }
    }

}
