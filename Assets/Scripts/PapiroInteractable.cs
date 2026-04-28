using UnityEngine;

public class PapiroInteractable : MonoBehaviour
{
    public GameObject tutorialPanel;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenTutorial();
        }

        else if (Input.GetKeyDown(KeyCode.C) && tutorialPanel.activeSelf)
        {
            CloseTutorial();
        }
    }

    void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.papyrusInteraction);
        Time.timeScale = 0f; // pausa el juego
    }

    void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.papyrusInteraction);
        Time.timeScale = 1f; // reanuda el juego
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}