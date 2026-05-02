using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("Video / Panel")]
    public VideoPlayer introVideoPlayer;
    public GameObject botonesMenu;

    [Header("Opciones (UI)")]
    public GameObject volumeSliderGO;
    public GameObject sfxSliderGO;
    public GameObject backButtonGO;

    [Header("Sliders (componentes)")]
    public Slider volumeSlider;
    public Slider sfxSlider;

    private string escenaDestino;

    private void Start()
    {
        // Evento cuando termina el video
        introVideoPlayer.loopPointReached += OnVideoEnd;

        // Asegura configuración correcta
        introVideoPlayer.playOnAwake = false;

        // Oculta opciones al inicio
        volumeSliderGO.SetActive(false);
        sfxSliderGO.SetActive(false);
        backButtonGO.SetActive(false);

        // Inicializa sliders
        if (AudioManager.Instance != null)
        {
            volumeSlider.value = AudioManager.Instance.GetMasterVolume();
            sfxSlider.value = AudioManager.Instance.GetSFXVolume();
        }

        volumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    // --- BOTÓN PLAY ---
    public void StartGame(string nombreEscena)
    {
        escenaDestino = nombreEscena;

        botonesMenu.SetActive(false);

        if (AudioManager.Instance != null)
            AudioManager.Instance.musicSource.Stop();

        // 🔥 Activa, prepara y reproduce el video correctamente
        introVideoPlayer.gameObject.SetActive(true);

        introVideoPlayer.Stop();
        introVideoPlayer.Prepare();

        introVideoPlayer.prepareCompleted += OnVideoPrepared;
    }

    // 🔥 Cuando el video está listo
    void OnVideoPrepared(VideoPlayer vp)
    {
        vp.prepareCompleted -= OnVideoPrepared; // evita duplicados
        vp.Play();
    }

    // 🔥 Cuando termina el video
    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(escenaDestino);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGameplayMusic();
    }

    public void OpenOptions()
    {
        botonesMenu.SetActive(false);
        volumeSliderGO.SetActive(true);
        sfxSliderGO.SetActive(true);
        backButtonGO.SetActive(true);
    }

    public void CloseOptions()
    {
        botonesMenu.SetActive(true);

        volumeSliderGO.SetActive(false);
        sfxSliderGO.SetActive(false);
        backButtonGO.SetActive(false);
    }

    // --- AUDIO ---

    public void OnMasterVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMasterVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetSFXVolume(value);
    }
}