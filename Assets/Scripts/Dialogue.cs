using UnityEngine;
using System.Collections;
using TMPro;   

public class Dialogue : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    private float typingSpeed = 0.05f;

    void Update()
    {
        if (isPlayerInRange)
        {
            if(!didDialogueStart)
            {
                startDialogue();
            }
        }
    }

    private void startDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entered dialogue range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player exited dialogue range");
            dialoguePanel.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
