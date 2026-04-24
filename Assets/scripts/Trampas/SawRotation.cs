using UnityEngine;

public class SawRotation : MonoBehaviour
{
    [SerializeField] private float speed = 300f;

    void Update()
    {
        transform.Rotate(0, 0, -speed * Time.deltaTime);
    }
}