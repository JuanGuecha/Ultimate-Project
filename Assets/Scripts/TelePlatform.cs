using UnityEngine;
using System.Collections;

public class TelePlatform : MonoBehaviour
{

    [SerializeField] private Transform teleportTarget;
    [SerializeField] private Collider2D TargetCollider;
    [SerializeField] private float teleportDelay = 0.5f;
    [SerializeField] private BuoyancyEffector2D buoyancy;
    public float TiempoBuoyancyEffect = 0.5f; // Tiempo que el efecto de buoyancy estará activo después de teletransportar


    void Start()
    {
        buoyancy = GetComponent<BuoyancyEffector2D>();
        if (buoyancy == null)
        {
            Debug.LogError($"TelePlatform: No se encontró un BuoyancyEffector2D en {gameObject.name}");
            enabled = false;
        }
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

        // Accedemos al BuoyancyEffector2D del objeto destino
        BuoyancyEffector2D targetEffector = teleportTarget.GetComponent<BuoyancyEffector2D>();
        
        if (targetEffector != null)
        {
            // Aquí puedes modificar propiedades, por ejemplo la densidad o superficie
            Debug.Log($"Accediendo al Buoyancy de {teleportTarget.name}: Nivel de superficie {targetEffector.surfaceLevel}");
        }
        playerTransform.position = teleportTarget.position;
        TargetCollider.enabled = false; // Desactivamos el collider del destino para evitar colisiones no deseadas al llegar
        targetEffector.enabled = true; // Activamos el Buoyancy del destino para que el jugador flote al llegar
        yield return new WaitForSeconds(TiempoBuoyancyEffect); // Pequeña espera para asegurar que el jugador se posicione antes de desactivar el Buoyancy del origen
        targetEffector.enabled = false;
        TargetCollider.enabled = true; // Desactivamos el Buoyancy del destino para que el jugador no flote indefinidamente
        Debug.Log("¡Viaje completado!");
    }
}
