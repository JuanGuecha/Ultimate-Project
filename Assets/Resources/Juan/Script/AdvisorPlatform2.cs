using UnityEngine;
using System.Collections;
using System.Data;

public class AdvisorPlatform2 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color color;
    private Coroutine flashCoroutine;
    public int timeActive; //Influye en la intermitencia y el tiempo que la plataforma permanece inactiva
    private Collider2D collider2d;
    int TouchPlayer = 1;
    public float timeColor; 
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
            
            if (TouchPlayer == 3 && flashCoroutine == null)
            {   
                flashCoroutine = StartCoroutine(DeletePlataform());
                Debug.Log("Player colisionó con la plataforma, iniciando cambio de color y temporizador");
            }
            else if(flashCoroutine == null)
            {
                StartCoroutine(AdvisorPlayer());
                Debug.Log("Player colisionó con la plataforma, iniciando cambio de color");
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player salió de la plataforma, reiniciando color y temporizador");
    
        }
    }
    IEnumerator AdvisorPlayer()
    {
        
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(timeColor);
        spriteRenderer.material.color = color;
        TouchPlayer++;
    }
    IEnumerator DeletePlataform()
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
    }
    private void ResetPlatform()
    {
        spriteRenderer.enabled = true;
        collider2d.enabled = true;
        Debug.Log("Plataforma restablecida después de " + timeActive + " segundos");
        spriteRenderer.material.color = color;
        TouchPlayer = 1;
        flashCoroutine = null;
    }

}