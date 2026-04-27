using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int collectedScarabFragments = 0;
    public int totalScarabFragments = 3;

    public ScarabUI scarabUI;
    // public DoorController finalDoor;

    public Transform centralHubSpawn; // 🔥 punto al que vuelve tras recoger fragmento
    public PlayerRespawn playerRespawn; // Referencia al PlayerRespawn para teletransportar al jugador al centro
    private PlayerInput playerInput; // Referencia al PlayerInput para detectar la tecla de pausa

    [Header("Paneles UI")]
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    private bool paused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (scarabUI != null)
        {
            scarabUI.UpdateScarabUI(collectedScarabFragments);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
            
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
            paused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
    }

    public void Resume()
    {
            paused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMenuMusic();
        }// 🔥 importante
        SceneManager.LoadScene("Christian"); // tu escena
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        Time.timeScale = 0f;
        Debug.Log("GAME OVER");
        AudioManager.Instance.musicSource.Stop(); // Detiene la música de fondo actual
        AudioManager.Instance.PlayGameOverMusic(); // Reproduce el sonido de game over
    }

    public void RetryFromCheckpoint()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (playerRespawn != null)
            playerRespawn.Respawn();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGameplayMusic();
    }

    public void CollectScarabFragment()
    {
        if (collectedScarabFragments >= totalScarabFragments)
            return;

        collectedScarabFragments++;

        Debug.Log("Fragmentos recogidos: " + collectedScarabFragments);

        if (scarabUI != null)
        {
            scarabUI.UpdateScarabUI(collectedScarabFragments);
        }

        TeleportToHub(); // Teletransporta al jugador al centro tras recoger un fragmento

        /*if (collectedScarabFragments >= totalScarabFragments)
        {
            if (finalDoor != null)
            {
                finalDoor.OpenDoor();
            }
        }*/
    }

    public void TeleportToHub()
    {
        StartCoroutine(TeleportCoroutine()); // Inicia la corrutina de teletransporte al centro tras recoger un fragmento
    }

    IEnumerator TeleportCoroutine()
    {
        yield return new WaitForSeconds(2f);
        if (playerRespawn != null)
        {
            playerRespawn.transform.position = centralHubSpawn.position; // Teletransporta al jugador al centro
            Debug.Log("Teletransportado al centro tras recoger fragmento");

        }
    }
}