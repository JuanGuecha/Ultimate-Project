using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int collectedScarabFragments = 0;
    public int totalScarabFragments = 3;

    public ScarabUI scarabUI;
    // public DoorController finalDoor;

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

        /*if (collectedScarabFragments >= totalScarabFragments)
        {
            if (finalDoor != null)
            {
                finalDoor.OpenDoor();
            }
        }*/
    }
}