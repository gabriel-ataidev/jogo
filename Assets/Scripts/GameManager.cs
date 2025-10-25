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
    [SerializeField] private int scoreToWin = 10;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Jogo fechado!");
            Application.Quit();
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

        if (score >= scoreToWin)
        {
            _gameEnded = true;
            PhaseFeedbackManager.Instance.ShowSuccessCSharp();
        }
    }

    public void EndGame()
    {
        if (_gameEnded) return;
        _gameEnded = true;
        Time.timeScale = 0;
        Debug.Log("O tempo acabou! Fim de jogo.");
    }

    public bool IsGameEnded()
    {
        return _gameEnded;
    }
}
