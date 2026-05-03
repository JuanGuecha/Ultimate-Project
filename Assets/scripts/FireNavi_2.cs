using UnityEngine;

public class FireNavi_2 : MonoBehaviour
{
    [SerializeField] private TriggerFire triggerFire;
    private Animator _animator;
    private bool _hasBeenActivated = false; // Para evitar llamar al trigger de la animación repetidamente

    void Start()
    {
        _animator = GetComponent<Animator>();
        
        // Validación Fail-Fast
        if (triggerFire == null) {
            Debug.LogError($"FireNavi_2 en {gameObject.name}: ¡No tiene asignado un TriggerFire!");
        }
    }

    void Update()
    {
        // 💡 Vigilamos si el trigger se activó y si aún no hemos encendido la animación
        if (triggerFire != null && triggerFire.IsActive && !_hasBeenActivated)
        {
            _animator.SetTrigger("On");
            _hasBeenActivated = true; // Marcamos como activado para no entrar más aquí
            Debug.Log($"<color=red>FUEGO:</color> {gameObject.name} activado por el trigger.");
        }
    }
}
