using UnityEngine;

public class FireNavi : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        if(triggerFire != null && triggerFire.IsActive)
        {
            animator.SetBool("On", true);
        }
    }
}
