using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float intensidad = 0.3f;
    public float duracion = 0.5f;
    private Vector3 posicionOriginal;
    
    public void Sacudir()
    {
        StartCoroutine(SacudirCamara());
    }
    
    IEnumerator SacudirCamara()
    {
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
    }
}