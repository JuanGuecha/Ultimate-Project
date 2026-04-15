using UnityEngine;
using UnityEngine.AI;
public class mummy : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;

    public Sprite mummySprite; // Asigna el sprite de la momia en el inspector


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 5f;
        agent.speed = 8f;
        agent.acceleration = 30;

        // Busca automáticamente al player por tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;
            spriteRenderer.sprite = mummySprite;
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
