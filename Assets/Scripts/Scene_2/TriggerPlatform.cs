using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    public bool PlatformTriggered = false;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            PlatformTriggered = true;
        }

    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            PlatformTriggered = false;
        }

    }   
}
