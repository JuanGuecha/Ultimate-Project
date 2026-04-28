using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{
    public string ZoneTag;


    private void OnTriggerEnter2D(Collider2D other)
    {

        ZoneTag = gameObject.tag;
        Debug.Log("Collision detected with: " + other.tag);
        if (other.CompareTag("Player"))
        {
            //SceneManager.LoadScene(ZoneTag);
            Debug.Log("Scene change triggered for zone: " + ZoneTag);
            SceneManager.LoadScene("Jefe Pruebas");
        }


    }
}
