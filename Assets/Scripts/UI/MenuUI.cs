using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void StartGame(string nombreEscena)
    {
        AudioManager.Instance.PlayGameplayMusic();
        SceneManager.LoadScene(nombreEscena);
    }
}
