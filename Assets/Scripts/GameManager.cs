using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int collectedScarabFragments = 0;
    public int totalScarabFragments = 3;

    public ScarabUI scarabUI;
    // public DoorController finalDoor;

    public Transform centralHubSpawn; // 🔥 punto al que vuelve tras recoger fragmento
    public PlayerRespawn playerRespawn; // Referencia al PlayerRespawn para teletransportar al jugador al centro

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (scarabUI != null)
        {
            scarabUI.UpdateScarabUI(collectedScarabFragments);
        }
    }

    public void CollectScarabFragment()
    {
        if (collectedScarabFragments >= totalScarabFragments)
            return;

        collectedScarabFragments++;

        Debug.Log("Fragmentos recogidos: " + collectedScarabFragments);

        if (scarabUI != null)
        {
            scarabUI.UpdateScarabUI(collectedScarabFragments);
        }

        TeleportToHub(); // Teletransporta al jugador al centro tras recoger un fragmento

        /*if (collectedScarabFragments >= totalScarabFragments)
        {
            if (finalDoor != null)
            {
                finalDoor.OpenDoor();
            }
        }*/
    }

    public void TeleportToHub()
    {
        StartCoroutine(TeleportCoroutine()); // Inicia la corrutina de teletransporte al centro tras recoger un fragmento
    }

    IEnumerator TeleportCoroutine()
    {
        yield return new WaitForSeconds(2f);
        if (playerRespawn != null)
        {
            playerRespawn.transform.position = centralHubSpawn.position; // Teletransporta al jugador al centro
            Debug.Log("Teletransportado al centro tras recoger fragmento");

        }
    }
}