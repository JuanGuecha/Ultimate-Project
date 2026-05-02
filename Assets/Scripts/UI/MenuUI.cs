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
    public GameObject volumeSliderGO;   // GameObject del slider
    public GameObject sfxSliderGO;      // GameObject del slider
    public GameObject backButtonGO;

    [Header("Sliders (componentes)")]
    public Slider volumeSlider;         // componente Slider
    public Slider sfxSlider;            // componente Slider

    private string escenaDestino;

    private void Start()
    {
        // Oculta opciones al inicio
        volumeSliderGO.SetActive(false);
        sfxSliderGO.SetActive(false);
        backButtonGO.SetActive(false);

        // Inicializa sliders con valores guardados
        if (AudioManager.Instance != null)
        {
            volumeSlider.value = AudioManager.Instance.GetMasterVolume();
            sfxSlider.value = AudioManager.Instance.GetSFXVolume();
        }

        // (Opcional) asegurar que los sliders llamen al AudioManager
        // Si ya lo hiciste por Inspector, puedes omitir estas líneas:
        volumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    // --- Botones del menú ---

    public void StartGame(string nombreEscena)
    {
        escenaDestino = nombreEscena;

        botonesMenu.SetActive(false);

        if (AudioManager.Instance != null)
            AudioManager.Instance.musicSource.Stop();

        // Si no usas video, cargas directo:
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

    // --- Callbacks de sliders ---

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