using System.Collections;
using UnityEngine;
public class EnemyBehaviour : MonoBehaviour
{
    public float detectionRange = 5f;
    public float speed = 5f;

    private Transform player;
    private Rigidbody2D rb;
    private EnemyPatrol patrol;
    public GameObject attackObject;
    bool isAttacking = false;

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

        if (distance < detectionRange && !isAttacking)
        {
            patrol.enabled = false;

            Vector2 direction = (player.position - transform.position).normalized;

            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        }
        else if (!isAttacking)
        {

            patrol.enabled = true;
        }
        if (distance < 1.5f && !isAttacking)
        {
            StartCoroutine(attack());
        }
    }
    IEnumerator attack()
    {

        isAttacking = true;
        Debug.Log("Enemigo Atacando");
        attackObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        attackObject.SetActive(false);
        isAttacking = false;
    }
}
