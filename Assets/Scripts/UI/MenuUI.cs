using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuUI : MonoBehaviour
{
    public VideoPlayer introVideoPlayer;
    public GameObject mainMenuPanel;

    private string escenaDestino;

    public void StartGame(string nombreEscena)
    {
        escenaDestino = nombreEscena;

        mainMenuPanel.SetActive(false);

        // 🔇 Apaga música del menú
        if (AudioManager.Instance != null)
            AudioManager.Instance.musicSource.Stop();

        // ▶️ Activa y reproduce video
        //introVideoPlayer.loopPointReached += OnVideoFinished;
        introVideoPlayer.gameObject.SetActive(true);
        SceneManager.LoadScene(escenaDestino);
        //  introVideoPlayer.Play();
        // AudioManager.Instance.PlayGameplayMusic();
    }

    /*private void OnVideoFinished(VideoPlayer vp)
    {
        introVideoPlayer.loopPointReached -= OnVideoFinished;

        // 🎵 Inicia música del gameplay
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGameplayMusic();

        // 🚀 Carga escena
        SceneManager.LoadScene(escenaDestino);
    }*/
}