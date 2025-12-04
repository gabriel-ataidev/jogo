using UnityEngine;
using TMPro;

public class PhaseFeedbackManager : MonoBehaviour
{
    public static PhaseFeedbackManager Instance;

    [Header("Painel de Sucesso")]
    [SerializeField] private GameObject panelSuccess;
    [SerializeField] private TMP_Text successTitle;
    [SerializeField] private TMP_Text successDescription;

    [Header("Painel de Game Over")]
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TMP_Text gameOverTitle;
    [SerializeField] private TMP_Text gameOverDescription;

    private bool waitingForInput = false;

    // Títulos de carreira baseados na fase
    private readonly string[] careerTitles = {
        "Estagiário",     
        "Junior",        
        "Pleno",          
        "Senior",         
        "DEUS DOS CÓDIGOS" 
    };

    private readonly string[] promotionMessages = {
        "Você foi contratado como <b>Estagiário</b>!\nBem-vindo à empresa!",
        "Você foi efetivado para <b>Junior</b>!\nAgora você tem vale refeição!",
        "Promoção! Você agora é <b>Pleno</b>!\nGanhou uma cadeira melhor!",
        "Impressionante! Você é <b>Senior</b>!\nAgora você pode reclamar do código dos outros!",
        "LENDÁRIO! Você é o <b>DEUS DOS CÓDIGOS</b>!\nTodos se curvam perante sua sabedoria!"
    };

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        HideAllPanels();
    }

    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current == null) return;

        if (waitingForInput && panelGameOver != null && panelGameOver.activeSelf)
        {
            if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                RestartCurrentPhase();
            }
            else if (UnityEngine.InputSystem.Keyboard.current.sKey.wasPressedThisFrame)
            {
                QuitGame();
            }
        }

        if (panelSuccess != null && panelSuccess.activeSelf)
        {
            if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                NextPhase();
            }
        }
    }

    void HideAllPanels()
    {
        if (panelSuccess != null) panelSuccess.SetActive(false);
        if (panelGameOver != null) panelGameOver.SetActive(false);
        waitingForInput = false;
    }

    string GetCurrentCareerTitle()
    {
        int phaseNumber = PhaseManager.Instance.GetCurrentPhaseNumber();
        int index = Mathf.Clamp(phaseNumber - 1, 0, careerTitles.Length - 1);
        return careerTitles[index];
    }

    string GetPromotionMessage()
    {
        int phaseNumber = PhaseManager.Instance.GetCurrentPhaseNumber();
        int index = Mathf.Clamp(phaseNumber - 1, 0, promotionMessages.Length - 1);
        return promotionMessages[index];
    }
    
    // ==================== SUCESSO ====================
    public void ShowSuccess(PhaseData phase)
    {
        if (panelSuccess == null)
        {
            Debug.LogError("PanelSuccess não está configurado no Inspector!");
            return;
        }

        Time.timeScale = 0;
        panelSuccess.SetActive(true);

        int currentPhase = PhaseManager.Instance.GetCurrentPhaseNumber();
        int totalPhases = PhaseManager.Instance.GetTotalPhases();
        bool isLastPhase = !PhaseManager.Instance.HasNextPhase();

        if (successTitle != null)
        {
            if (isLastPhase)
            {
                successTitle.text = "VOCÊ VENCEU!";
            }
            else
            {
                successTitle.text = "PROMOÇÃO!";
            }
        }

        if (successDescription != null)
        {
            string text = "";

            text += $"<size=64>{GetPromotionMessage()}</size>\n\n";

            text += $"<color=#AAAAAA>Linguagem dominada:</color>\n";
            text += $"<b><size=80><color=#00FF88>{phase.linguagem}</color></size></b>\n";
            text += $"<color=#888888>{phase.descricao}</color></size>\n\n";

            // Progresso
            text += $"<color=#FFCC00>Fase {currentPhase}/{totalPhases} concluída!</color>\n\n";

            // Instrução
            if (isLastPhase)
            {
                text += "<color=#FFFFFF>Pressione <b>ESPAÇO</b> para jogar novamente</color>";
            }
            else
            {
                text += "<color=#FFFFFF>Pressione <b>ESPAÇO</b> para o próximo desafio</color>";
            }

            successDescription.text = text;
        }
    }

    // ==================== GAME OVER ====================
    
    public void ShowGameOver(PhaseData phase)
    {
        if (panelGameOver == null)
        {
            Debug.LogError("PanelGameOver não está configurado no Inspector!");
            return;
        }

        Time.timeScale = 0;
        panelGameOver.SetActive(true);
        waitingForInput = true;

        if (gameOverTitle != null)
            gameOverTitle.text = "DEMITIDO!";

        if (gameOverDescription != null)
        {
            string text = "<size=64><b>Você foi desligado da empresa!</b></size>\n\n";
            text += "<color=#FF6666>\"Passe na sala do RH e pegue suas coisas...\"</color>\n\n";
            
            text += $"<color=#AAAAAA>Motivo da demissão:</color>\n";
            text += $"Confundiu as linguagens de programação!\n\n";
            
            text += $"<color=#AAAAAA>A linguagem correta era:</color>\n";
            text += $"<b><size=80><color=#00AAFF>{phase.linguagem}</color></size></b>\n\n";
            
            text += $"<color=#888888>{phase.descricao}</color></size>\n\n";

            text += "<color=#FFFFFF>Pressione <b>ESPAÇO</b> para tentar recontratação\n";
            text += "Pressione <b>S</b> para desistir da carreira</color>";

            gameOverDescription.text = text;
        }
    }

    // ==================== PRAZO ESTOURADO ====================
    public void ShowTimeOut(PhaseData phase)
    {
        if (panelGameOver == null)
        {
            Debug.LogError("PanelGameOver não está configurado no Inspector!");
            return;
        }

        Time.timeScale = 0;
        panelGameOver.SetActive(true);
        waitingForInput = true;

        if (gameOverTitle != null)
            gameOverTitle.text = "PRAZO ESTOURADO!";

        if (gameOverDescription != null)
        {
            string text = "<size=64><b>Você perdeu o deadline!</b></size>\n\n";
            text += "<color=#FF6666>\"O cliente cancelou o contrato...\"</color>\n\n";

            text += $"<color=#AAAAAA>Você estava trabalhando com:</color>\n";
            text += $"<b><size=80><color=#00AAFF>{phase.linguagem}</color></size></b>\n\n";

            text += $"<color=#888888>{phase.descricao}</color></size>\n\n";

            text += "<color=#FFFFFF>Pressione <b>ESPAÇO</b> para tentar de novo\n";
            text += "Pressione <b>S</b> para desistir</color>";

            gameOverDescription.text = text;
        }
    }

    // ==================== AÇÕES ====================

    public void NextPhase()
    {
        if (PhaseManager.Instance.HasNextPhase())
        {
            PhaseManager.Instance.NextPhase();
        }
        else
        {
            PhaseManager.Instance.RestartAllPhases();
        }
        RestartGame();
    }

    public void RestartCurrentPhase()
    {
        waitingForInput = false;
        RestartGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
