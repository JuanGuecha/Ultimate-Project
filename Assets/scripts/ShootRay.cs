using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootRay : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public LayerMask enemyLayer;
    PlayerInput playerInput;
    bool charged = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && charged)
        {
            Vector2 direction = shootPosition();
            StartCoroutine(shoot(direction));
        }
        if (playerInput.actions["Ray"].WasPressedThisFrame())
        {
            charged = true;
            Debug.Log("Cargado");
        }
    }

    Vector3 shootPosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPos.z = 0;
        Vector2 direction = mouseWorldPos - transform.position;
        return direction;
    }
    IEnumerator shoot(Vector2 direction)
    {

        Vector2 pos = transform.position;
        float RaycastLenght = direction.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(pos, direction.normalized, RaycastLenght, enemyLayer);
        Debug.DrawRay(pos, direction.normalized * RaycastLenght, Color.red, 1f);
        Debug.Log("ray");
        if (hit.collider != null && hit.collider.tag == "Enemy")
        {
            Debug.Log("raycasthit");
            EnemyBehaviour enemigo = hit.collider.GetComponent<EnemyBehaviour>();//comportamiento noraml
            mummy momificado = hit.collider.GetComponent<mummy>();//script momificado
            Rigidbody2D enemyRb = hit.collider.GetComponent<Rigidbody2D>();
            enemigo.enabled = false;
            momificado.enabled = true;
            enemyRb.simulated = false;// desactiva el simulated para que de la sensacion de flotar hacia el jugador
        }
        else if (hit.collider != null)
        {
            Debug.Log("Golpeó a: " + hit.collider.name);
        }
        else
        {
            Debug.Log("No golpeó nada");
        }
        lineRenderer.SetPosition(0, pos);
        lineRenderer.SetPosition(1, pos + direction);
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.4f);

        lineRenderer.enabled = false;
        charged = false;
    }
}
