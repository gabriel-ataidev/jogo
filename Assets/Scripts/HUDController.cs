using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    private TMP_Text phaseText;
    private TMP_Text scoreText;
    private TMP_Text timerText;
    private TMP_Text timerTitle;
    private TMP_Text scoreTitle;
    private TMP_Text descriptionText;
    private TMP_Text yearText;

    void Start()
    {
        FindHUDElements();
        ApplyYellowBlackStyle();
        // Aguardar um frame para garantir que PhaseManager está inicializado
        Invoke(nameof(UpdatePhaseText), 0.1f);
    }

    void FindHUDElements()
    {
        // Buscar elementos dinamicamente por nome
        TMP_Text[] allTexts = GetComponentsInChildren<TMP_Text>(true);
        
        foreach (TMP_Text txt in allTexts)
        {
            string name = txt.gameObject.name.ToLower();
            
            if (name.Contains("phase") || name.Contains("fase"))
                phaseText = txt;
            else if (name.Contains("score") && !name.Contains("title") && !name.Contains("titulo"))
                scoreText = txt;
            else if (name.Contains("timer") && !name.Contains("title") && !name.Contains("titulo"))
                timerText = txt;
            else if (name.Contains("timer") && (name.Contains("title") || name.Contains("titulo")))
                timerTitle = txt;
            else if (name.Contains("score") && (name.Contains("title") || name.Contains("titulo")))
                scoreTitle = txt;
            else if (name.Contains("description") || name.Contains("descri"))
                descriptionText = txt;
            else if (name.Contains("year") || name.Contains("ano"))
                yearText = txt;
        }
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        if (scoreText != null)
            scoreText.text = $"<color=#FFFFFF><b>{GameManager.Instance.score}</b></color>";

        if (timerText != null)
            timerText.text = $"<color=#FFFFFF><b>{GameManager.Instance.remainingTime:F0}s</b></color>";
        
        // Atualizar informações da fase constantemente caso mudem
        if (PhaseManager.Instance != null && Time.frameCount % 30 == 0) // Atualizar a cada 30 frames
        {
            UpdatePhaseText();
        }
    }

    void ApplyYellowBlackStyle()
    {
        // Aplicar estilo amarelo e preto aos títulos (sem emojis para evitar problemas de fonte)
        if (timerTitle != null)
            timerTitle.text = "<color=#fff>TEMPO</color>";
        
        if (scoreTitle != null)
            scoreTitle.text = "<color=#fff>PONTOS</color>";
    }

    public void UpdatePhaseText()
    {
        if (PhaseManager.Instance == null)
        {
            Debug.Log("PhaseManager.Instance é null ao tentar atualizar HUD");
            return;
        }

        PhaseData currentPhase = PhaseManager.Instance.GetCurrentPhase();
        if (currentPhase == null)
        {
            Debug.Log("currentPhase é null ao tentar atualizar HUD");
            return;
        }

        int phaseNumber = PhaseManager.Instance.GetCurrentPhaseNumber();
        int totalPhases = PhaseManager.Instance.GetTotalPhases();
        
        Debug.Log($"Atualizando HUD: Fase {phaseNumber}/{totalPhases} - {currentPhase.linguagem}");
        
        if (phaseText != null)
        {
            phaseText.text = $"<color=#FFFFFF><b>FASE {phaseNumber}/{totalPhases}</b></color> \n<color=#FFFFFF>{currentPhase.linguagem}</color>";
        }

        // Atualizar descrição da linguagem
        if (descriptionText != null)
        {
            descriptionText.text = $"<color=#FFFFFF>{currentPhase.descricao}</color>";
        }

        // Atualizar ano de lançamento
        if (yearText != null)
        {
            yearText.text = $"<color=#FFFFFF>Lançamento:</color> <color=#FFFFFF><b>{currentPhase.anoDeLancamento}</b></color>";
        }
    }
}
