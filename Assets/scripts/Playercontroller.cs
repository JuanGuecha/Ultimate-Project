using System.Text.RegularExpressions;
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
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerCollider = GetComponent<Collider2D>();
        isGrounded = true;
        rb.sharedMaterial = defaultMaterial;

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
    }


    void move()
    {
        Vector2 moveVector = playerInput.actions["move"].ReadValue<Vector2>();
        if (playerInput.actions["move"].IsPressed())
        {

            rb.linearVelocity = new Vector2(moveVector.x * movespeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        if (moveVector.y < -0.5f)//presionar hacia abajo para bajar de la plataforma
        {
            GetDownPlatform();
        }
    }
    void jump()
    {
        if (playerInput.actions["Jump"].WasPressedThisFrame() && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Platform") && !isIgnoringCollision)
        {
            isGrounded = true;
            currentPlatform = collision.gameObject;

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
    }

    void GetDownPlatform()
    {
        Debug.Log("Moving downwards, ignoring collision");
        Physics2D.IgnoreCollision(playerCollider, currentPlatform.GetComponent<Collider2D>(), true);
        isIgnoringCollision = true;
    }
}
