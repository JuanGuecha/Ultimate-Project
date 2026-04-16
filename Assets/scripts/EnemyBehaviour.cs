using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using Unity.Mathematics;
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
    Vector2 startposition; //al inicio detecta donde esta puesta la momia
    public float bound; // distancia hasta la que puede ir la momia tanto a izquierda como a derecha
    public bool isOutOfBounds;
    public float rangedetection;

    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2f;
        agent.speed = 5f;
        agent.acceleration = 15;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        startposition = transform.position;
        isOutOfBounds = false;

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


        movement(target);
    }


    void movement(Transform target)
    {
        Vector2 distance = target.position - transform.position;

        if (distance.magnitude < rangedetection && isOutOfBounds == false)
        {
            agent.SetDestination(target.position);
            Debug.Log("Persiguiendo al jugador");
        }
        if (transform.position.x > startposition.x + bound || transform.position.x < startposition.x - bound)
        {
            Debug.Log("Volviendo a posicion incial");
            isOutOfBounds = true;
            agent.SetDestination(startposition);
            agent.stoppingDistance = 0f;
        }
        if (math.floor(transform.position.x) == math.floor(startposition.x))
        {
            Debug.Log("Volvio a la posicion incial");
            isOutOfBounds = false;
            agent.stoppingDistance = 2f;
        }
    }
}
