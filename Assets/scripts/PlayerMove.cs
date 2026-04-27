using UnityEngine;
using UnityEngine.InputSystem; // Necesario para el nuevo Input System


public class PlayerMovement : MonoBehaviour
{
    
    // Movimiento base y salto con buffer.
    [Header("Configuración de Movimiento")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 16f;
    [SerializeField] private float jumpBufferTime = 0.15f;
    
    // Detección de suelo con BoxCast.
    [Header("Detección de Colisiones")]
    [Tooltip("Puedes seleccionar una o varias capas para detección de suelo.")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float detectionDistance = 0.1f;

    // Gizmo para depurar el ground check.
    [Header("Gizmo Ground Check")]
    [SerializeField] private Vector2 groundCheckSize = new Vector2(1f, 0.2f);
    [SerializeField] private Vector2 groundCheckOffset = new Vector2(0f, -0.6f);
    [SerializeField] private Color groundCheckColor = Color.yellow;

    // Soporte para plataformas.
    [Header("Plataformas")]
    [SerializeField] private string movingPlatformTag = "MovilPlatform";
    //[SerializeField] private string solidPlatformTag = "SolidPlatform";

    // Materiales opcionales por si luego se reactiva fricción especial.
    /*
    [Header("Materiales de Friccion")]
    [SerializeField] private PhysicsMaterial2D defaultMaterial;
    [SerializeField] private PhysicsMaterial2D frictionMaterial;
    */

    // Referencias cacheadas.
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    // Estado de movimiento y plataforma actual.
    private float horizontalInput;
    private float verticalInput;
    private float jumpBufferCounter;
    private Transform currentMovingPlatform;

    // Acciones del Input System.
    private InputAction jumpAction;
    private PlayerInput playerInput;

    private void Awake()
    {
        // Cache inicial de componentes y acciones.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];

    }

    private void Update()
    {
        // Lee el input de movimiento.
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        horizontalInput = moveInput.x;
        verticalInput = moveInput.y;

        // Voltea el sprite sin alterar la escala.
        if (horizontalInput > 0.01f)
            spriteRenderer.flipX = false;
        else if (horizontalInput < -0.01f)
            spriteRenderer.flipX = true;

        // Jump buffer para hacer el salto más responsivo.
        if (jumpAction != null && jumpAction.WasPressedThisFrame())
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Aplica el movimiento horizontal.
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Ejecuta el salto cuando el buffer y el suelo coinciden.
        if (jumpBufferCounter > 0f && IsGrounded())
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            transform.SetParent(null);
            currentMovingPlatform = null;
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("jump");
            jumpBufferCounter = 0f;
        }
    }

    // Comprueba suelo con un BoxCast ajustado al collider.
    private bool IsGrounded()
    {
        Vector2 boxCenter = (Vector2)boxCollider.bounds.center + groundCheckOffset;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCenter, groundCheckSize, 0, Vector2.down, detectionDistance, groundLayers);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Plataforma móvil: enparentar solo si contacto desde arriba.
        if (collision.gameObject.CompareTag(movingPlatformTag) && collision.GetContact(0).normal.y > 0.5f)
        {
            currentMovingPlatform = collision.transform;
            transform.SetParent(currentMovingPlatform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Soltar plataforma móvil.
        if (collision.transform == currentMovingPlatform)
        {
            transform.SetParent(null);
            currentMovingPlatform = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el volumen del ground check en Scene.
        BoxCollider2D selectedBoxCollider = GetComponent<BoxCollider2D>();

        if (selectedBoxCollider == null)
        {
            return;
        }

        Gizmos.color = groundCheckColor;

        Vector3 boxCenter = selectedBoxCollider.bounds.center + (Vector3)groundCheckOffset;
        Vector3 castEndCenter = boxCenter + Vector3.down * detectionDistance;
        Gizmos.DrawWireCube(castEndCenter, groundCheckSize);
    }
}



