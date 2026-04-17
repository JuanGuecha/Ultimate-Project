using UnityEngine;


public class SceneChange : MonoBehaviour
{
    public string ZoneTag;
    SceneController sceneController;
    void Start()
    {
        sceneController = GameObject.Find("GameManager").GetComponent<SceneController>();
        if (sceneController == null)
        {
            Debug.LogError("SceneController not found in the scene. Please ensure there is a GameObject named 'GameManager' with the SceneController component attached.");
        }    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        ZoneTag = gameObject.tag;
        Debug.Log("Collision detected with: " + other.tag);
        if (other.CompareTag("Player"))
        {
            sceneController.loadScene(ZoneTag);
            Debug.Log("Scene change triggered for zone: " + ZoneTag);
        }
            
    }
}
