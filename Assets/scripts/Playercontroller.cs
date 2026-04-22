using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Playercontroller : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerInput playerInput;
    Collider2D playerCollider;
    public float movespeed;
    public float jumpforce;
    bool isIgnoringCollision;
    GameObject currentPlatform;
    bool isGrounded;
    PhysicsMaterial2D physicsMaterial2D;
    public PhysicsMaterial2D defaultMaterial;
    public PhysicsMaterial2D frictionMaterial;
    SpriteRenderer spriteRenderer;

    [Header("Animaciones")]
    Animator animator;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerCollider = GetComponent<Collider2D>();
        isGrounded = true;
        rb.sharedMaterial = defaultMaterial;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    void Start()

    {


    }

    void Update()
    {
        // Movimiento e Input: Polling cada frame para respuesta inmediata
        move();
        jump();
        
        // Actualizamos parámetros del Animator
        // 'isGround' controla si estamos tocando el suelo (Idle/Run balance)
        animator.SetBool("isGround", isGrounded);
        
        // El parámetro 'VerticalVelocity' ayuda a diferenciar entre subir (salto) y caer (gravedad)
        // Similar a cómo los animadores de Disney exageran la caída para dar sensación de peso
    
    }

    void move()
    {
        // Nota: Asegúrate de que en el Input Action la acción se llame exactamente "Move"
        Vector2 moveVector = playerInput.actions["Move"].ReadValue<Vector2>();
        
        if (playerInput.actions["Move"].IsPressed())
        {
            animator.SetFloat("Horizontal", Math.Abs(moveVector.x));
            rb.linearVelocity = new Vector2(moveVector.x * movespeed, rb.linearVelocity.y);
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        if (moveVector.y < -0.5f && currentPlatform != null)
        {
            GetDownPlatform();
        }
    }

    void jump()
    {
        // En Unity, leer el input en WasPressed permite un solo salto por pulsación
        if (playerInput.actions["Jump"].WasPressedThisFrame())
        {
            if (isGrounded)
            {
                // Disparamos un Trigger para el salto (mejor que bool si la animación es corta)
                // Es como en los juegos de lucha, una acción instantánea que activa una secuencia
                animator.SetTrigger("Jump");
                
                isGrounded = false; // "Forzamos" la salida del suelo para la animación
                animator.SetBool("isGround", false);
                
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Usamos tags para identificar superficies. Ground, Platform y Saliente reinician el salto.
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Saliente"))
        {
            isGrounded = true;
            // Al tocar suelo, reseteamos el trigger por si acaso y activamos isGround
            animator.SetBool("isGround", true);
            animator.ResetTrigger("Jump");
        }

        if (collision.gameObject.CompareTag("Platform") && !isIgnoringCollision)
        {
            currentPlatform = collision.gameObject;
            // Parentesco dinámico: Como en los niveles de trenes en Uncharted, 
            // nos pegamos a la plataforma para heredar su inercia.
            transform.SetParent(collision.transform);
        }

        if (collision.gameObject.CompareTag("Saliente"))
        {
            rb.sharedMaterial = frictionMaterial;
        }

        if (currentPlatform != null)
        {
            Physics2D.IgnoreCollision(playerCollider, currentPlatform.GetComponent<Collider2D>(), false);
        }

        isIgnoringCollision = false;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Al salir de una superficie de apoyo, marcamos que ya no estamos en suelo.
        // Esto activará la animación de "caída" o "vuelo" si la velocidad vertical es negativa.
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Saliente"))
        {
            isGrounded = false;
            animator.SetBool("isGround", false);
            
            if (collision.gameObject.CompareTag("Platform"))
            {
                transform.SetParent(null);
            }

            if (collision.gameObject.CompareTag("Saliente"))
            {
                rb.sharedMaterial = defaultMaterial;
            }
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && !isIgnoringCollision)
        {
            isGrounded = true;
            currentPlatform = collision.gameObject;

        }
    }

    void GetDownPlatform()
    {
        Debug.Log("Moving downwards, ignoring collision");
        Physics2D.IgnoreCollision(playerCollider, currentPlatform.GetComponent<Collider2D>(), true);
        isIgnoringCollision = true;
    }
}

