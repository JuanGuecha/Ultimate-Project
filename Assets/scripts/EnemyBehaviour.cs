using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour
{
    // Transform player;
    // Rigidbody2D rb;
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     player = GameObject.Find("Player").transform;
    //     rb = GetComponent<Rigidbody2D>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     Vector2 playerDirection = player.position - transform.position;
    //     rb.linearVelocity = new Vector2(playerDirection.normalized.x * 5f, rb.linearVelocity.y);
    // }
    private Transform target;
    private NavMeshAgent agent;
    public Material momifiedMaterial;
    private Renderer renderer;
    Vector2 distance;

    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2f;
        agent.speed = 5f;
        agent.acceleration = 15;

        // Busca automáticamente al player por tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con tag 'Player'");
        }
    }


    void Update()
    {
        if (target == null) return; // 👈 sale inmediatamente

        Vector2 distance = target.position - transform.position;

        if (distance.magnitude < 7f)
        {
            agent.SetDestination(target.position);
        }
    }


}
