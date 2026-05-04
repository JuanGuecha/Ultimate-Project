using UnityEngine;
using TMPro;
using LLMUnity;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

/// <summary>
/// Gestiona la interacción por proximidad con Bastet utilizando LLMUnity.
/// Implementa pre-generación de respuestas para eliminar latencia.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Myscript : MonoBehaviour // Asegúrate que el archivo se llame Myscript.cs
{
    [Header("References")]
    [SerializeField] private LLMAgent _agent;
    [SerializeField] private TMP_Text _barkText;
    [SerializeField] private GameObject _MarkDialogue;
    [SerializeField] GameObject Panel;

    [Header("Input Settings")]
    [SerializeField] private InputActionReference _interactAction;

    [Header("Interaction Settings")]
    [Tooltip("Tiempo mínimo entre interacciones")]
    [SerializeField] private float _cooldown = 5f;
    [Tooltip("Tag del objeto jugador")]
    [SerializeField] private string _playerTag = "Player";

    private string _cachedBark;
    private bool _isGenerating;
    private bool _canTrigger = true;
    private bool _playerIsInRange = false;
    private float _nextTriggerTime;

    private void OnEnable()
    {
        if (_interactAction != null) _interactAction.action.Enable();

        if (_agent == null || _barkText == null)
        {
            Debug.LogError($"<color=red>⚠️ {name}:</color> Faltan referencias por asignar.");
            enabled = false;
        }
    }

    private void OnDisable()
    {
        if (_interactAction != null) _interactAction.action.Disable();
    }

    private async void Start()
    {
        // Limpieza inicial
        _barkText.text = "";
        
        // Espera de seguridad para inicialización de LLMUnity
        await Task.Delay(1000); 
        
        if (enabled) await PreGenerateBark();
    }

    private void Update()
    {
        // Solo interactuamos si estamos en rango y presionamos el botón
        if (_playerIsInRange && _interactAction != null && _interactAction.action.WasPressedThisFrame())
        {
            if (_canTrigger && !string.IsNullOrEmpty(_cachedBark))
            {
                DisplayBark();
            }
            else if (_isGenerating)
            {
                Debug.Log("<color=orange>⌛ Bastet está meditando una respuesta todavía...</color>");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_playerTag))
        {
            _playerIsInRange = true;
             // 💡 Solo activamos la burbuja si el secreto ya está listo
            if (!string.IsNullOrEmpty(_cachedBark))
            {
                _MarkDialogue.SetActive(true);
            }
            Debug.Log("<color=cyan>✨ Bastet siente tu presencia.</color>");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_playerTag))
        {
            _playerIsInRange = false;
            _MarkDialogue.SetActive(false);
            _barkText.text = ""; // Feedback visual: el mensaje desaparece al alejarse
            Panel.SetActive(false); // Aseguramos que el panel también se oculte al salir del rango
        }
    }

    private void DisplayBark()
    {
        // Activamos panel de Canvas para mostrar el mensaje
        Panel.SetActive(true);
        _barkText.text = _cachedBark;
        _cachedBark = null; // Vaciamos inmediatamente
        _MarkDialogue.SetActive(false);
        _canTrigger = false;
        _nextTriggerTime = Time.time + _cooldown;

        // Disparamos la nueva generación sin esperar (fire-and-forget)
        _ = PreGenerateBark();
    }

    private async Task PreGenerateBark()
    {
        if (_isGenerating) return;
        _isGenerating = true;

        try
        {
            // Llamada asíncrona al modelo Llama-3.1-8B
            string response = await _agent.Chat("");
            // 💡 Limpieza Profunda:
            // Añadimos comillas simples, dobles (rectas y curvas) y puntos finales si quieres que sea solo la frase.
            char[] charsToTrim = { '"', '\'', ' ', '\n', '\r', '¨', '”', '“', '‘', '’', '.' };
            _cachedBark = response.Trim(charsToTrim);
            
            Debug.Log($"<color=green>✅ Bastet tiene un nuevo secreto preparado.</color>");
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"<color=orange>⚠️ Fallo en red neuronal:</color> {ex.Message}");
        }
        finally
        {
            _isGenerating = false;
        }
    }

    private void LateUpdate()
    {
        if (!_canTrigger && Time.time >= _nextTriggerTime)
            _canTrigger = true;
    }
}