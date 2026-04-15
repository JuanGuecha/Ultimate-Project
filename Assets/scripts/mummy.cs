using UnityEngine;
using UnityEngine.AI;
public class mummy : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    public Material momifiedMaterial;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 5f;
        agent.speed = 8f;
        agent.acceleration = 30;

        // Busca automáticamente al player por tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;
            renderer.material = momifiedMaterial;
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con tag 'Player'");
        }
    }

    void Update()
    {
        if (target == null) return;

        agent.SetDestination(target.position);
    }
}
