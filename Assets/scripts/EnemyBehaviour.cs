using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerDirection = player.position - transform.position;
        rb2d.AddForce((playerDirection * 0.1f).normalized);
    }
}
