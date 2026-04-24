using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    SceneController sceneController;
    Playercontroller playerController;
    public string checkpointTag;
    void Start()
    {
        //sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        playerController = GameObject.Find("Player").GetComponent<Playercontroller>();
    }

    void OnTriggerEnter2D(Collider2D collision) // "collision" es el objeto que entra en el trigger
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Checkpoint alcanzado por el jugador.");
            checkpointTag = gameObject.tag; // Asumiendo que el checkpoint tiene un tag específico como "SpawnPoint_1_1"
            //playerController.spawnTag = checkpointTag; // Asigna el tag del checkpoint al jugador
        }
    }
}
