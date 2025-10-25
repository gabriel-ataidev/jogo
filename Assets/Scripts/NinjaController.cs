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
        if (GameManager.Instance.IsGameEnded())
            return;

        string name = other.gameObject.name;

        if (name.Contains("CSharp"))
            GameManager.Instance.AddScore(1);
        else if (name.Contains("JavaScript"))
            PhaseFeedbackManager.Instance.ShowErrorJS();
        else if (name.Contains("Python"))
            PhaseFeedbackManager.Instance.ShowErrorPython();

        Destroy(other.gameObject);
    }
}
