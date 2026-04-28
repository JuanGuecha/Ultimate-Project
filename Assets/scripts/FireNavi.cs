using UnityEngine;
using System.Collections;

public class FireNavi : MonoBehaviour
{
    // [SerializeField] private TriggerFire triggerFire;
    private Animator animator;
    public float timeWait;
    //public float minR, maxR;
    public float flashLapes;
    Color color;
    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        /*if (triggerFire == null)
            {
                Debug.LogError($"FireNavi: TriggerFire no encontrado en {gameObject.name}");
            }*/
        color = spriteRenderer.color;
        StartCoroutine(ActivateFire());
    }


    IEnumerator ActivateFire()
    {
        while (true)
        {
            // 1. ESPERA INICIAL (Opcional, para que no empiece nada más cargar la escena)
            //float timeFire = Random.Range(minR, maxR);


            // 2. INTERMITENCIA (Aviso)
            float flashDuration = 0.15f;
            for (int i = 0; i < flashLapes; i++)
            {
                spriteRenderer.material.color = new Color(1f, 0f, 0f, 1f); // (Red, Green, Blue, Alpha)
                yield return new WaitForSeconds(flashDuration);
                spriteRenderer.material.color = color;
                yield return new WaitForSeconds(flashDuration);
            }

            // 3. ¡FUEGO!
            animator.SetTrigger("ON");
            yield return new WaitForSeconds(timeWait);

        }

    }
}
