using UnityEngine;
using UnityEngine.UI;

public class ScarabUI : MonoBehaviour
{
    public Image[] fragments;

    public float inactiveAlpha = 0.3f;
    public float activeAlpha = 1f;

    public void UpdateScarabUI(int collectedFragments)
    {
        for (int i = 0; i < fragments.Length; i++)
        {
            Color color = fragments[i].color;

            if (i < collectedFragments)
                color.a = activeAlpha;
            else
                color.a = inactiveAlpha;

            fragments[i].color = color;
        }
    }
}
