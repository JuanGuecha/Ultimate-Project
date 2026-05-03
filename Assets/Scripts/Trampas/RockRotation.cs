using UnityEngine;

public class RockRotation : MonoBehaviour
{
    [SerializeField] private float speed = 300f;
    public float rotationDirection = 1f; // 1 para sentido horario, -1 para antihorario

    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime * rotationDirection);
    }
}
