using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    Vector2 startposition;

    void Start()
    {
        startposition = transform.position;
        rb = GetComponent<Rigidbody2D>();

        Vector2 direction = transform.right;
        rb.linearVelocity = direction * speed;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = startposition;
        gameObject.SetActive(false);

    }


}
