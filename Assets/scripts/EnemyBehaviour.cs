using UnityEngine;
public class EnemyBehaviour : MonoBehaviour
{
    public float detectionRange = 5f;
    public float speed = 5f;

    private Transform player;
    private Rigidbody2D rb;
    private EnemyPatrol patrol;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        patrol = GetComponent<EnemyPatrol>();

        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
            player = obj.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            patrol.enabled = false;

            Vector2 direction = (player.position - transform.position).normalized;

            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        }
        else
        {

            patrol.enabled = true;
        }
    }
}
