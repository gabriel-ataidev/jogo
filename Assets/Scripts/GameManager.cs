using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public float remainingTime = 60f;

    private bool _gameEnded = false;
    private bool _timerRunning = false;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int scoreToWin = 3;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                Debug.Log("Jogo fechado!");
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }
    }

    public void StartTimer()
    {
        if (!_timerRunning && !_gameEnded)
        {
            _timerRunning = true;
            StartCoroutine(GameTimer());
        }
    }

    IEnumerator GameTimer()
    {
        while (remainingTime > 0 && !_gameEnded)
        {
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
            if (timerText != null)
                timerText.text = $"{remainingTime:F0}s";
        }

        remainingTime = 0;
        EndGame();
    }

    public void AddScore(int value)
    {
        if (_gameEnded) return;

        score += value;
        if (scoreText != null)
            scoreText.text = score.ToString();

        Debug.Log($"Score atual: {score}/{scoreToWin}");

        if (score >= scoreToWin)
        {
            _gameEnded = true;
            Debug.Log("Atingiu score necessário! Mostrando tela de sucesso...");
            
            if (PhaseManager.Instance == null)
            {
                Debug.LogError("PhaseManager.Instance é null!");
                return;
            }
            
            PhaseData currentPhase = PhaseManager.Instance.GetCurrentPhase();
            
            if (currentPhase == null)
            {
                Debug.LogError("currentPhase é null!");
                return;
            }
            
            if (PhaseFeedbackManager.Instance == null)
            {
                Debug.LogError("PhaseFeedbackManager.Instance é null!");
                return;
            }
            
            Debug.Log($"Chamando ShowSuccess para fase: {currentPhase.linguagem}");
            PhaseFeedbackManager.Instance.ShowSuccess(currentPhase);
        }
    }

    public void EndGame()
    {
        if (_gameEnded) return;
        _gameEnded = true;
        Time.timeScale = 0;
        Debug.Log("O tempo acabou! Fim de jogo.");
        
        // Mostrar game over por tempo esgotado
        if (PhaseManager.Instance != null && PhaseFeedbackManager.Instance != null)
        {
            PhaseData currentPhase = PhaseManager.Instance.GetCurrentPhase();
            if (currentPhase != null)
            {
                PhaseFeedbackManager.Instance.ShowTimeOut(currentPhase);
            }
        }
    }

    public bool IsGameEnded()
    {
        return _gameEnded;
    }

    public void ResetForNextPhase()
    {
        score = 0;
        remainingTime = 60f;
        _gameEnded = false;
        _timerRunning = false;
        
        if (scoreText != null)
            scoreText.text = "0";
        if (timerText != null)
            timerText.text = "60s";
    }
}
