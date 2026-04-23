using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Debug.Log(collision.gameObject.name + "Muerto");
        }
    }
}
