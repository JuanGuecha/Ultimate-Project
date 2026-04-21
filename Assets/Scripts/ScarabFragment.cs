using UnityEngine;

public class ScarabFragment : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.CompareTag("Player"))
        {
            collected = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.CollectScarabFragment();
            }

            gameObject.SetActive(false);
        }
    }
}