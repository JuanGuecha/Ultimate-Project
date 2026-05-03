using UnityEngine;

public class FireNavi_2 : MonoBehaviour
{
    [SerializeField] private TriggerFire triggerFire;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        if (triggerFire == null){
            Debug.LogError($"FireNavi: TriggerFire no encontrado en {gameObject.name}");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && triggerFire.IsActive)
        {
            animator.SetTrigger("On");
        }
    }
}
