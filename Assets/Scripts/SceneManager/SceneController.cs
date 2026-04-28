
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class SceneController : MonoBehaviour
{
    public string zoneTag;
    public string checkpointTag;
    Playercontroller playerController;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<Playercontroller>();
    }
    public void loadScene(string zoneTag)
    {
        
        Debug.Log($"🔍 zoneTag recibido: '{zoneTag}' (largo: {zoneTag.Length})");
        
        if (zoneTag == "Zona5")
        {
        SceneManager.LoadScene(sceneBuildIndex: 2); 
        }
    }
    
    public void CheckPoint(string checkpointTag)
    {
        Debug.Log($"🔍 checkpointTag recibido: '{checkpointTag}' (largo: {checkpointTag.Length})");

        if (checkpointTag == "Zona_1_2")
        {
            StartCoroutine(Respawn(checkpointTag));
        }
         if (checkpointTag == "Zona_2_1")
        {
            StartCoroutine(Respawn(checkpointTag));
        }
         if (checkpointTag == "Zona_2_2")
        {
            StartCoroutine(Respawn(checkpointTag));
        }
         if (checkpointTag == "Zona_3_1")
        {
            StartCoroutine(Respawn(checkpointTag));
        }
         if (checkpointTag == "Zona_3_2")
        {
            StartCoroutine(Respawn(checkpointTag));
        }
        if (checkpointTag == "Zona_4_1")
        {
            StartCoroutine(Respawn(checkpointTag));
        }
         if (checkpointTag == "Zona_4_2")
        {
            StartCoroutine(Respawn(checkpointTag));
        }
    }
    IEnumerator Respawn(string checkpointTag)
    {
        yield return new WaitForSeconds(2f);
        transform.position = GameObject.FindGameObjectWithTag(checkpointTag).transform.position; //
    }
    
    
}
