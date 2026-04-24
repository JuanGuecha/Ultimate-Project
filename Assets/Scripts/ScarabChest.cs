using UnityEngine;
using System.Collections;

public class ScarabChest : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite closedChestSprite;
    public Sprite openChestSprite;

    public float openDelay = 0.3f;

    private bool collected = false;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (closedChestSprite != null)
            spriteRenderer.sprite = closedChestSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.CompareTag("Player"))
        {
            collected = true;
            StartCoroutine(OpenChestRoutine());
        }
    }

    private IEnumerator OpenChestRoutine()
    {
        // Cambia visualmente el cofre a abierto
        if (openChestSprite != null)
            spriteRenderer.sprite = openChestSprite;

        Debug.Log("Cofre abierto. Fragmento obtenido.");

        // Espera un momento para que se vea el cofre abierto
        yield return new WaitForSeconds(openDelay);

        // Suma el fragmento al GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectScarabFragment();
        }

         gameObject.SetActive(false); // Desactiva el cofre para que no se pueda interactuar más

        // Opcional: desactivar collider para que no se pueda recoger otra vez
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }
}