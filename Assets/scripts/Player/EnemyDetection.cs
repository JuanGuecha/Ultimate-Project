using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public IsisHealth isisHealth;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Debug.Log(collision.gameObject.name + "Muerto");
        }
        if (collision.CompareTag("Isis"))
        {
            isisHealth.isisTakeDamage(10f);
        }
    }
}
