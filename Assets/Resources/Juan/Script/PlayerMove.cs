using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Sistema de movimiento Hollow Knight - Metroidvania 2D
/// Sencillo, completo y compatible con TilemapCollider
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [Header("Componentes")]
    private PlayerInput _playerInput;
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    
    [Header("Movimiento")]
    [SerializeField] private float _maxSpeed = 6f;
    [SerializeField] private float _acceleration = 30f;
    [SerializeField] private float _friction = 25f;
    
    [Header("Salto")]
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private int _coyoteFrames = 6;
    
    [Header("Física")]
    [SerializeField] private float _fallingGravity = 2f;
    
    // Estado
    private float _velocityX;
    private int _coyoteCounter;
    private bool _wasJumping;
    
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        
        if (!_playerInput) Debug.LogWarning("[PlayerMove] Falta PlayerInput", this);
        if (!_rb) Debug.LogWarning("[PlayerMove] Falta Rigidbody2D", this);
        if (!_collider) Debug.LogWarning("[PlayerMove] Falta BoxCollider2D", this);
    }
    
    void Update()
    {
        if (!_playerInput || !_rb) return;
        
        // Leer input
        float inputX = _playerInput.actions["Move"].ReadValue<Vector2>().x;
        bool jumpPressed = _playerInput.actions["Jump"].IsPressed();
        
        // Detectar suelo
        bool isGrounded = IsGrounded();
        
        // Coyote time
        if (isGrounded)
            _coyoteCounter = _coyoteFrames;
        else
            _coyoteCounter--;
        
        // Movimiento horizontal suave
        if (inputX != 0)
            _velocityX = Mathf.Lerp(_velocityX, inputX * _maxSpeed, Time.deltaTime * _acceleration);
        else
            _velocityX = Mathf.Lerp(_velocityX, 0, Time.deltaTime * _friction);
        
        // Salto (presiona = salta, suelta = cae)
        if (jumpPressed && !_wasJumping && _coyoteCounter > 0)
        {
            _rb.linearVelocity = new Vector2(_velocityX, _jumpForce);
            _wasJumping = true;
        }
        
        if (!jumpPressed)
            _wasJumping = false;
        
        // Aplicar velocidad X
        _rb.linearVelocity = new Vector2(_velocityX, _rb.linearVelocity.y);
    }
    
    void FixedUpdate()
    {
        if (!_rb) return;
        
        // Gravedad variable: cae más rápido que sube
        if (_rb.linearVelocity.y < 0)
            _rb.gravityScale = _fallingGravity;
        else
            _rb.gravityScale = 1f;
    }
    
    /// <summary>
    /// Detecta si está tocando suelo (funciona con TilemapCollider)
    /// </summary>
    private bool IsGrounded()
    {
        if (!_collider) return false;
        
        Vector2 checkPos = (Vector2)transform.position + _collider.offset + 
                          Vector2.down * (_collider.size.y * 0.5f + 0.05f);
        
        // OverlapBox debajo del player - detecta TODO
        Collider2D[] hits = Physics2D.OverlapBoxAll(checkPos, 
                                                    new Vector2(_collider.size.x * 0.9f, 0.1f), 0);
        
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject != gameObject)
                return true;
        }
        
        return false;
    }
}
