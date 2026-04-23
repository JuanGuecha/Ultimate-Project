using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyBehaviour : MonoBehaviour
{
    public float detectionRange = 5f;
    public float speed = 5f;

    private Transform player;
    private Rigidbody2D rb;
    private EnemyPatrol patrol;
    public GameObject attackObject;
    bool isAttacking = false;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        patrol = GetComponent<EnemyPatrol>();
        anim = GetComponentInChildren<Animator>();

        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
            player = obj.transform;

        if (SceneManager.GetActiveScene().name == "Jefe Pruebas")//Para que reviva en la escena de jefe
        {
            StartCoroutine(Revive());
        }
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
        float vx = rb.linearVelocity.x;

        if (vx > 0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (vx < -0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
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


    IEnumerator Revive()
    {
        patrol.enabled = false;
        isAttacking = true;
        anim.Play("Revive");
        anim.SetBool("Isreviving", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Isreviving", false);
        isAttacking = false;



    }
}
