using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public PlayerMana playerMana;

    public float meleeCost = 15f;
    public float rangedCost = 25f;
    private PlayerInput playerInput;
    public GameObject MeeleePoint;
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public LayerMask enemyLayer;
    public float rayDistance = 10f;
    Playercontroller playerController;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerController = GetComponent<Playercontroller>();


    }
    void Update()
    {
        if (playerInput.actions["MeleeAttack"].triggered)
        {
            MeleeAttack();
        }

        if (playerInput.actions["RangedAttack"].triggered)
        {
            RangedAttack();
        }
    }

    void MeleeAttack()
    {
        if (playerMana.UseMana(meleeCost))
        {
            StartCoroutine(MeleeRoutine());
        }
        else
        {
            Debug.Log("No hay suficiente mana");
        }
    }

    void RangedAttack()
    {
        if (playerMana.UseMana(rangedCost))
        {
            Debug.Log("Ataque a distancia");

            float directionX = playerController.facingDirection;
            Vector2 direction = new Vector2(directionX, 0);

            StartCoroutine(ShootRay(direction));

        }
        else
        {
            Debug.Log("No hay suficiente mana");
        }
    }

    IEnumerator ShootRay(Vector2 direction)
    {
        Vector2 origin = firePoint.position;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance, enemyLayer);

        Debug.DrawRay(origin, direction * rayDistance, Color.red, 1f);

        if (hit.collider != null)
        {
            Debug.Log("Golpeó a: " + hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
        else
        {
            Debug.Log("No golpeó nada");
        }

        // Visual del rayo
        lineRenderer.SetPosition(0, origin);

        if (hit.collider != null)
            lineRenderer.SetPosition(1, hit.point);
        else
            lineRenderer.SetPosition(1, origin + direction * rayDistance);

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.2f);

        lineRenderer.enabled = false;
    }

    IEnumerator MeleeRoutine()
    {
        Vector3 pos = MeeleePoint.transform.localPosition;
        pos.x = Mathf.Abs(pos.x) * playerController.facingDirection;
        MeeleePoint.transform.localPosition = pos;
        MeeleePoint.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        MeeleePoint.SetActive(false);
    }
}