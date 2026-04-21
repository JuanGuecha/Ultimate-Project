using UnityEngine;

public class DoorController : MonoBehaviour
{
    public void OpenDoor()
    {
        Debug.Log("Puerta abierta");
        gameObject.SetActive(false);
    }
}
