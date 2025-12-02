using UnityEngine;
using UnityEngine.InputSystem;

public class NinjaController : MonoBehaviour
{
    [SerializeField] private float xSpeed = 8f;
    private Rigidbody2D _rb;
    private float xDir;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
            GameManager.Instance.AddScore(1);
        }
        else
        {
            // Errou - game over
            Debug.Log("Errou!");
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
}
