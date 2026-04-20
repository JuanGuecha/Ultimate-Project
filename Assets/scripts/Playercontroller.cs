using System;
using System.Collections;
using System.Text.RegularExpressions;
using NUnit.Framework;
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
    public bool isGrounded;
    PhysicsMaterial2D physicsMaterial2D;
    public PhysicsMaterial2D defaultMaterial;
    public PhysicsMaterial2D frictionMaterial;
    public GameObject attackpoint;
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
        //Movimiento
        move();
        //Salto
        jump();
        animator.SetBool("isGround", isGrounded);
        //Ataque
        if (playerInput.actions["Attack"].WasPressedThisFrame())
        {
            StartCoroutine(MeleeAttack());
        }


    }


    void move()
    {
        Vector2 moveVector = playerInput.actions["move"].ReadValue<Vector2>();
        if (playerInput.actions["move"].IsPressed())
        {
            animator.SetFloat("Horizontal", Math.Abs(moveVector.x));
            rb.linearVelocity = new Vector2(moveVector.x * movespeed, rb.linearVelocity.y);
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        if (moveVector.y < -0.5f && currentPlatform != null)//presionar hacia abajo para bajar de la plataforma
        {
            GetDownPlatform();
        }
        if (moveVector.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    void jump()
    {
        if (playerInput.actions["Jump"].WasPressedThisFrame())
        {
            animator.SetTrigger("Jump");
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
            }

        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("MovingPlatform") && !isIgnoringCollision)
        {
            isGrounded = true;
            currentPlatform = collision.gameObject;
            transform.SetParent(collision.transform); // Esto se utiliza para que el jugador pueda moverse en las plataformas móviles sin problemas, 
            // ya que el jugador se convierte en hijo de la plataforma y se mueve junto con ella.

        }
        if (collision.gameObject.CompareTag("Saliente"))
        {
            rb.sharedMaterial = frictionMaterial;
            isGrounded = true;


        }
        if (currentPlatform != null)
        {
            Physics2D.IgnoreCollision(playerCollider, currentPlatform.GetComponent<Collider2D>(), false);
        }

        isIgnoringCollision = false;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") ||
        collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
            transform.SetParent(null); //Cuando deja de estar en la plataforma, se establece el padre del jugador a null para que ya no siga el movimiento de la plataforma.
        }
        if (collision.gameObject.CompareTag("Saliente"))
        {
            rb.sharedMaterial = defaultMaterial;
            isGrounded = false;
        }

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && !isIgnoringCollision)
        {
            isGrounded = true;
            currentPlatform = collision.gameObject;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void GetDownPlatform()
    {
        Debug.Log("Moving downwards, ignoring collision");
        Physics2D.IgnoreCollision(playerCollider, currentPlatform.GetComponent<Collider2D>(), true);
        isIgnoringCollision = true;
    }
    IEnumerator MeleeAttack()
    {
        attackpoint.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackpoint.SetActive(false);
        Debug.Log("Ataque terminado");
    }

}

