using UnityEngine;
using System.Collections;

public class TelePlatform : MonoBehaviour
{
    // 💡 Teletransportador básico con tiempo de espera y desactivación temporal del collider destino para evitar loops.
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private Collider2D TargetCollider;
    [SerializeField] private float teleportDelay = 0.5f;
    public float TiempoColliderActive; // Tiempo que el collider del destino estará desactivado para evitar teletransportes inmediatos de vuelta
    [SerializeField] private Animator   animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 💡 Fail-fast: Si no configuraste el target, avisamos para no romper el juego
        if (teleportTarget == null)
        {
            Debug.LogWarning($"¡Ojo! {gameObject.name} no tiene un teleportTarget asignado.");
            return;
        }

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TeleportRoutine(collision.transform));
        }
    }
    private IEnumerator TeleportRoutine(Transform playerTransform)
    {
        Debug.Log("Teletransportando...");
        
        // 🪄 Aquí podrías añadir una llamada a tu Fade de pantalla o una partícula
        yield return new WaitForSeconds(teleportDelay);
        playerTransform.position = teleportTarget.position;
        Debug.Log("¡Teletransportación completa!");
        TargetCollider.enabled = false; // Desactivamos el collider del destino para evitar volver a teletransportar al jugador inmediatamente
        animator.SetBool("PortalState", true);
        yield return new WaitForSeconds(TiempoColliderActive); // Esperamos un tiempo antes de reactivar el collider 
        TargetCollider.enabled = true; // Reactivamos el collider del destino
        animator.SetBool("PortalState", false);
        Debug.Log("¡Plataforma de teletransporte activada!");
    }
}
