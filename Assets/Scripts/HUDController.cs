using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text phaseText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text timerTitle;
    [SerializeField] private TMP_Text scoreTitle;

    [SerializeField] private string nomeDaFase = "Fase 1: Linguagens OO";

    void Start()
    {
        if (phaseText != null)
            phaseText.text = nomeDaFase;
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        if (scoreText != null)
            scoreText.text = GameManager.Instance.score.ToString();

        if (timerText != null)
            timerText.text = $"{GameManager.Instance.remainingTime:F0}s";
    }
}
