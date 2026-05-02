using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Menu and Gameplay Music")]
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;
    public AudioClip gameOverMusic;

    [Header("Sound Effects")]
    public AudioClip playerDamage;
    public AudioClip playerDeath;
    public AudioClip chestOpen;
    public AudioClip noMana;
    public AudioClip meleeAttack;
    public AudioClip rangedAttack;
    public AudioClip jumpMovement;
    public AudioClip papyrusInteraction;
    public AudioClip mummyScream;
    public AudioClip rockFalling;
    public AudioClip torchIgnite;

    [Header("Volumes")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private void Awake()
    {
        Debug.Log("AudioManager Awake");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

            ApplyVolumes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("AudioManager Start");
        PlayMenuMusic();
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
        ApplyVolumes();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
        ApplyVolumes();
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    private void ApplyVolumes()
    {
        if (musicSource != null)
            musicSource.volume = masterVolume;

        if (sfxSource != null)
            sfxSource.volume = masterVolume * sfxVolume;
    }

    public void PlayMenuMusic()
    {
        Debug.Log("Intentando reproducir música de menú");
        PlayMusic(menuMusic);
    }

    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }

    public void PlayGameOverMusic()
    {
        PlayMusic(gameOverMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("El AudioClip está vacío.");
            return;
        }

        if (musicSource == null)
        {
            Debug.LogError("Music Source no está asignado.");
            return;
        }

        Debug.Log("Reproduciendo música: " + clip.name);

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;

        // IMPORTANTE: ya no usamos 0.5f fijo
        musicSource.volume = masterVolume;

        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.volume = masterVolume * sfxVolume;
            sfxSource.PlayOneShot(clip);
        }
    }
}