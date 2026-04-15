using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerDirection = player.position - transform.position;
        rb.linearVelocity = new Vector2(playerDirection.normalized.x * 5f, rb.linearVelocity.y);
    }
}
