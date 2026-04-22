using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image[] eyes;
    [SerializeField] private Sprite openEye;
    [SerializeField] private Sprite closedEye;

    public void UpdateHealth(int currentLives)
    {
        for (int i = 0; i < eyes.Length; i++)
        {
            if (i < currentLives)
                eyes[i].sprite = openEye;
            else
                eyes[i].sprite = closedEye;
        }
    }
}
