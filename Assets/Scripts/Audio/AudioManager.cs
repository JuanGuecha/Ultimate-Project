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
    //public AudioClip fragmentCollected;
    public AudioClip chestOpen;
    //public AudioClip doorOpen;
    public AudioClip noMana;
    public AudioClip meleeAttack;
    public AudioClip rangedAttack;
    public AudioClip jumpMovement;
    public AudioClip papyrusInteraction;
    public AudioClip mummyScream;
    public AudioClip rockFalling;
    public AudioClip torchIgnite;

    private void Awake()
    {
        Debug.Log("AudioManager Awake");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        musicSource.volume = 0.5f;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }
}