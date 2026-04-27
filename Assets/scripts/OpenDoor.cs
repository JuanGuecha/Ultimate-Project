using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private TriggerDoor triggerDoor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        animator = GetComponent<Animator>();
        
       
        if (triggerDoor == null){
            Debug.LogError($"OpenDoor: TriggerDoor no encontrado en {gameObject.name}");
        }

    }

    void Update()
    {
        if(triggerDoor != null && triggerDoor.IsOpen)
        {
            animator.SetBool("Open", true);
        }
    }


}
