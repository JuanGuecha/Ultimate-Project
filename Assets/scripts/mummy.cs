using UnityEngine;
using UnityEngine.AI;
public class mummy : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;

    public Sprite mummySprite; // Asigna el sprite de la momia en el inspector
    NavMeshAgent navMeshAgent;
    [SerializeField] GameObject player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;



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

        agent.SetDestination(target.transform.position);
    }
}
