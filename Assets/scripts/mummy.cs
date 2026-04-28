using UnityEngine;
using UnityEngine.AI;
public class mummy : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;

    public Sprite mummySprite; // Asigna el sprite de la momia en el inspector
    NavMeshAgent navMeshAgent;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

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
        if (target == null) return;

        agent.SetDestination(target.position);
    }
}
