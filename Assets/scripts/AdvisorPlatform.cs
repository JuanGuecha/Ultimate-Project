using UnityEngine;
using System.Collections;

public class AdvisorPlatform : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color color;
    private Coroutine flashCoroutine;
    public float timeActive;
    private Collider2D collider2d;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        collider2d = GetComponent<Collider2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (flashCoroutine == null)
            {
                flashCoroutine = StartCoroutine(ChangePlatformColor());
                Debug.Log("Player colisionó con la plataforma, iniciando cambio de color y temporizador");
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
                spriteRenderer.material.color = color;
                flashCoroutine = null;
                Debug.Log("Player salió de la plataforma, deteniendo cambio de color y temporizador");
            }
        }
    }
    IEnumerator ChangePlatformColor()
    {
        float flashDuration = 0.15f;
        
        for (int i = 0; i < timeActive; i++)
        {
            spriteRenderer.material.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.material.color = color;
            yield return new WaitForSeconds(flashDuration);
        }
        spriteRenderer.enabled = false;
        collider2d.enabled = false;
        Invoke("ResetPlatform", timeActive);
        flashCoroutine = null;
    }
    private void ResetPlatform()
    {
        spriteRenderer.enabled = true;
        collider2d.enabled = true;
        Debug.Log("Plataforma restablecida después de " + timeActive + " segundos");
        spriteRenderer.material.color = color;
    }

}