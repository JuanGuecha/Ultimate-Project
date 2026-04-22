using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float intensidad = 0.3f;
    public float duracion = 0.5f;
    private Vector3 posicionOriginal;
    [SerializeField] private TriggerCameraShake triggerCameraShake;
    public bool isShaking = false;

    void Update()
    {
        // Revisamos si el trigger se activó para iniciar la sacudida
        if (triggerCameraShake != null && triggerCameraShake.isTrigger && !isShaking)
        {
            Debug.Log("Iniciando sacudida de cámara desde CameraShake.cs");
            Sacudir();
        }
    }

    public void Sacudir()
    {
        if (!isShaking)
        {
            StartCoroutine(SacudirCamara());
        }
    }
    
    IEnumerator SacudirCamara()
    {
        isShaking = true;
        posicionOriginal = transform.localPosition;
        float tiempoPasado = 0f;
        
        while (tiempoPasado < duracion)
        {
            float x = Random.Range(-1f, 1f) * intensidad;
            float y = Random.Range(-1f, 1f) * intensidad;
            transform.localPosition = posicionOriginal + new Vector3(x, y, 0);
            tiempoPasado += Time.deltaTime;
            yield return null;
        }
        
        transform.localPosition = posicionOriginal;
        isShaking = false;
    }
}