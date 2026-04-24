using System.Collections;
using UnityEngine;

public class IsisMovement : MonoBehaviour
{
    public GameObject TpPoints;
    public float teleportInterval = 4f;
    public float fadeSpeed = 2f;

    private Transform[] points;
    private int currentIndex = 0;

    private SpriteRenderer[] renderers;

    void Start()
    {
        // Obtener todos los puntos hijos
        points = new Transform[TpPoints.transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = TpPoints.transform.GetChild(i);
        }

        renderers = GetComponentsInChildren<SpriteRenderer>();

        StartCoroutine(TeleportLoop());
    }

    IEnumerator TeleportLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(teleportInterval);

            yield return StartCoroutine(FadeTeleport());
        }
    }

    IEnumerator FadeTeleport()
    {
        // FADE OUT
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            SetAlpha(1f - t);
            yield return null;
        }

        // Cambiar punto (rotación simple)
        currentIndex = (currentIndex + 1) % points.Length;

        transform.position = points[currentIndex].position;

        // FADE IN
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            SetAlpha(t);
            yield return null;
        }
    }

    void SetAlpha(float alpha)
    {
        foreach (var sr in renderers)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }
}