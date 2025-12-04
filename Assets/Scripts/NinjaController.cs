using UnityEngine;
using UnityEngine.InputSystem;

public class NinjaController : MonoBehaviour
{
    [SerializeField] private float xSpeed = 8f;
    
    [Header("Limites de Movimento (baseado na câmera)")]
    [SerializeField] private bool useScreenBounds = true;
    [SerializeField] private float minX = -2.5f;
    [SerializeField] private float maxX = 2.5f;
    [SerializeField] private float boundsPadding = 0.3f;
    
    [Header("Efeitos Sonoros")]
    [SerializeField] private AudioClip sfxCorrect;
    [SerializeField] private AudioClip sfxWrong;
    [SerializeField] [Range(0f, 1f)] private float sfxVolume = 1f;
    
    private Rigidbody2D _rb;
    private float xDir;
    private Camera _mainCamera;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }

    void Start()
    {
        // Calcular limites automaticamente baseado na câmera
        if (useScreenBounds && _mainCamera != null)
        {
            CalculateScreenBounds();
        }
    }

    void CalculateScreenBounds()
    {
        // Para câmera ortográfica, calcula os limites baseado no tamanho ortográfico
        if (_mainCamera.orthographic)
        {
            float screenAspect = (float)Screen.width / Screen.height;
            float cameraHeight = _mainCamera.orthographicSize * 2;
            float cameraWidth = cameraHeight * screenAspect;
            
            minX = -cameraWidth / 2 + boundsPadding;
            maxX = cameraWidth / 2 - boundsPadding;
        }
        else
        {
            // Para câmera em perspectiva, usa a posição Y do player para calcular
            float distance = Mathf.Abs(_mainCamera.transform.position.z - transform.position.z);
            float screenHeight = 2.0f * distance * Mathf.Tan(_mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float screenWidth = screenHeight * _mainCamera.aspect;
            
            minX = -screenWidth / 2 + boundsPadding;
            maxX = screenWidth / 2 - boundsPadding;
        }
        
        Debug.Log($"Limites do player calculados: minX={minX}, maxX={maxX}");
    }

    void OnMove(InputValue inputValue)
    {
        xDir = inputValue.Get<Vector2>().x;
    }

    void FixedUpdate()
    {
        Vector2 velocity = _rb.linearVelocity;
        velocity.x = xDir * xSpeed;
        _rb.linearVelocity = velocity;
        
        // Limitar posição dentro dos bounds
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PhaseData currentPhase = PhaseManager.Instance.GetCurrentPhase();
        if (currentPhase == null)
        {
            Debug.LogError("currentPhase é null!");
            return;
        }

        string objectName = other.gameObject.name.ToLower();
        string correctLanguage = currentPhase.linguagem.ToLower().Replace(" ", "").Replace("(", "").Replace(")", "");

        Debug.Log($"Coletado: {objectName} | Correto: {correctLanguage}");

        // Verificar se o objeto coletado é a linguagem correta
        if (objectName.Contains(correctLanguage))
        {
            // Acertou - adiciona ponto
            Debug.Log("Acertou!");
            PlaySound(sfxCorrect);
            GameManager.Instance.AddScore(1);
        }
        else
        {
            // Errou - game over
            Debug.Log("Errou!");
            PlaySound(sfxWrong);
            if (PhaseFeedbackManager.Instance != null)
            {
                PhaseFeedbackManager.Instance.ShowGameOver(currentPhase);
            }
            else
            {
                Debug.LogError("PhaseFeedbackManager.Instance é null!");
            }
        }

        Destroy(other.gameObject);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, sfxVolume);
        }
    }
}
