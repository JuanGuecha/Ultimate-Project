using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    SceneChange SceneChange;

    void Start()
    {
        SceneChange = GameObject.FindWithTag("Zona2").GetComponent<SceneChange>();
    }
    public void loadScene(string zoneTag)
    {
        
        Debug.Log($"🔍 zoneTag recibido: '{zoneTag}' (largo: {zoneTag.Length})");
        
        if (zoneTag == "Zona2")
        {
        SceneManager.LoadScene(sceneBuildIndex: 2); 
        }
        else if (zoneTag == "Zona3")
        {
            SceneManager.LoadScene(sceneBuildIndex: 3);
        }
        else if (zoneTag == "Zona4")
        {
            SceneManager.LoadScene(sceneBuildIndex: 4);
        }
        else if (zoneTag == "Zona5")
        {
            SceneManager.LoadScene(sceneBuildIndex: 5);
        }
        else
        {
            Debug.LogWarning($"⚠️ Tag no reconocido: '{zoneTag}'");
        }
    }
}
