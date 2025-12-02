using UnityEngine;
using TMPro;
using System.Collections;

public class PhaseFeedbackManager : MonoBehaviour
{
    public static PhaseFeedbackManager Instance;

    [SerializeField] private GameObject panelSuccess;
    [SerializeField] private TMP_Text successTitle;
    [SerializeField] private TMP_Text successDescription;
    [SerializeField] private GameObject nextPhaseButton;
    [SerializeField] private GameObject restartButton;

    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TMP_Text gameOverTitle;
    [SerializeField] private TMP_Text gameOverDescription;

    private bool waitingForInput = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        FindPanels();
        HideAllPanels();
    }

    void FindPanels()
    {
        // Buscar painéis dinamicamente se não estiverem configurados
        if (panelSuccess == null)
        {
            panelSuccess = GameObject.Find("PanelSuccess");
            if (panelSuccess == null)
            {
                // Criar painel dinamicamente
                Debug.LogWarning("PanelSuccess não encontrado. Tentando buscar por nome alternativo...");
                GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
                foreach (GameObject obj in allObjects)
                {
                    if (obj.name.ToLower().Contains("success") || obj.name.ToLower().Contains("sucesso"))
                    {
                        panelSuccess = obj;
                        Debug.Log($"Painel de sucesso encontrado: {obj.name}");
                        break;
                    }
                }
                
                // Se ainda não encontrou, criar um painel básico
                if (panelSuccess == null)
                {
                    Debug.LogWarning("Criando PanelSuccess dinamicamente...");
                    CreateSuccessPanel();
                }
            }
        }

        if (panelGameOver == null)
        {
            panelGameOver = GameObject.Find("PanelGameOver");
            if (panelGameOver == null)
            {
                GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
                foreach (GameObject obj in allObjects)
                {
                    if (obj.name.ToLower().Contains("gameover") || obj.name.ToLower().Contains("game over"))
                    {
                        panelGameOver = obj;
                        Debug.Log($"Painel de game over encontrado: {obj.name}");
                        break;
                    }
                }
                
                // Se ainda não encontrou, criar um painel básico
                if (panelGameOver == null)
                {
                    Debug.LogWarning("Criando PanelGameOver dinamicamente...");
                    CreateGameOverPanel();
                }
            }
        }

        // Buscar textos se não configurados
        if (panelSuccess != null)
        {
            TMP_Text[] texts = panelSuccess.GetComponentsInChildren<TMP_Text>(true);
            foreach (TMP_Text txt in texts)
            {
                string name = txt.gameObject.name.ToLower();
                if (name.Contains("title") || name.Contains("titulo"))
                    successTitle = txt;
                else if (name.Contains("description") || name.Contains("descri"))
                    successDescription = txt;
            }
        }

        if (panelGameOver != null)
        {
            TMP_Text[] texts = panelGameOver.GetComponentsInChildren<TMP_Text>(true);
            foreach (TMP_Text txt in texts)
            {
                string name = txt.gameObject.name.ToLower();
                if (name.Contains("title") || name.Contains("titulo"))
                    gameOverTitle = txt;
                else if (name.Contains("description") || name.Contains("descri"))
                    gameOverDescription = txt;
            }
        }
    }

    void CreateSuccessPanel()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Nenhum Canvas encontrado na cena!");
            return;
        }

        panelSuccess = new GameObject("PanelSuccess");
        panelSuccess.transform.SetParent(canvas.transform, false);
        
        RectTransform rect = panelSuccess.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        
        UnityEngine.UI.Image img = panelSuccess.AddComponent<UnityEngine.UI.Image>();
        img.color = new Color(0, 0, 0, 0.9f);

        GameObject titleObj = new GameObject("SuccessTitle");
        titleObj.transform.SetParent(panelSuccess.transform, false);
        successTitle = titleObj.AddComponent<TextMeshProUGUI>();
        RectTransform titleRect = titleObj.GetComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0.5f, 0.7f);
        titleRect.anchorMax = new Vector2(0.5f, 0.7f);
        titleRect.sizeDelta = new Vector2(800, 100);
        successTitle.fontSize = 48;
        successTitle.alignment = TextAlignmentOptions.Center;

        GameObject descObj = new GameObject("SuccessDescription");
        descObj.transform.SetParent(panelSuccess.transform, false);
        successDescription = descObj.AddComponent<TextMeshProUGUI>();
        RectTransform descRect = descObj.GetComponent<RectTransform>();
        descRect.anchorMin = new Vector2(0.5f, 0.5f);
        descRect.anchorMax = new Vector2(0.5f, 0.5f);
        descRect.sizeDelta = new Vector2(900, 400);
        successDescription.fontSize = 24;
        successDescription.alignment = TextAlignmentOptions.Center;
        
        panelSuccess.SetActive(false);
        Debug.Log("PanelSuccess criado com sucesso!");
    }

    void CreateGameOverPanel()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Nenhum Canvas encontrado na cena!");
            return;
        }

        panelGameOver = new GameObject("PanelGameOver");
        panelGameOver.transform.SetParent(canvas.transform, false);
        
        RectTransform rect = panelGameOver.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        
        UnityEngine.UI.Image img = panelGameOver.AddComponent<UnityEngine.UI.Image>();
        img.color = new Color(0.2f, 0, 0, 0.9f);

        GameObject titleObj = new GameObject("GameOverTitle");
        titleObj.transform.SetParent(panelGameOver.transform, false);
        gameOverTitle = titleObj.AddComponent<TextMeshProUGUI>();
        RectTransform titleRect = titleObj.GetComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0.5f, 0.7f);
        titleRect.anchorMax = new Vector2(0.5f, 0.7f);
        titleRect.sizeDelta = new Vector2(800, 100);
        gameOverTitle.fontSize = 48;
        gameOverTitle.alignment = TextAlignmentOptions.Center;

        GameObject descObj = new GameObject("GameOverDescription");
        descObj.transform.SetParent(panelGameOver.transform, false);
        gameOverDescription = descObj.AddComponent<TextMeshProUGUI>();
        RectTransform descRect = descObj.GetComponent<RectTransform>();
        descRect.anchorMin = new Vector2(0.5f, 0.5f);
        descRect.anchorMax = new Vector2(0.5f, 0.5f);
        descRect.sizeDelta = new Vector2(900, 400);
        gameOverDescription.fontSize = 24;
        gameOverDescription.alignment = TextAlignmentOptions.Center;
        
        panelGameOver.SetActive(false);
        Debug.Log("PanelGameOver criado com sucesso!");
    }

    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current != null)
        {
            // Espera por input na tela de game over
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
            
            // Espera por espaço na tela de sucesso
            if (panelSuccess != null && panelSuccess.activeSelf)
            {
                if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    NextPhase();
                }
            }
        }
    }

    void HideAllPanels()
    {
        if (panelSuccess != null) panelSuccess.SetActive(false);
        if (panelGameOver != null) panelGameOver.SetActive(false);
        waitingForInput = false;
    }

    public void ShowSuccess(PhaseData phase)
    {
        Debug.Log("ShowSuccess chamado!");
        
        if (panelSuccess == null)
        {
            Debug.LogError("panelSuccess é null!");
            return;
        }
        
        Time.timeScale = 0;
        panelSuccess.SetActive(true);
        Debug.Log("Panel de sucesso ativado!");

        if (successTitle != null)
            successTitle.text = "PARABÉNS!";

        if (successDescription != null)
        {
            string descriptionText = $@"<b>Você completou a fase: {phase.linguagem}!</b>

<color=#FFFFFF>Informações da linguagem:</color>
{phase.descricao}

<b>Ano de lançamento:</b> {phase.anoDeLancamento}

Fase {PhaseManager.Instance.GetCurrentPhaseNumber()}/{PhaseManager.Instance.GetTotalPhases()} concluída!";

            // Mostrar informações da próxima fase se houver
            if (!PhaseManager.Instance.HasNextPhase())
            {
                descriptionText += "\n\n<color=#FFC107>Você completou todas as fases! Parabéns, ninja!</color>";
            }

            // Adicionar instrução para pressionar espaço
            if (PhaseManager.Instance.HasNextPhase())
            {
                descriptionText += "\n\n<color=#FFFFFF>Pressione <b>ESPAÇO</b> para iniciar a próxima fase</color>";
            }
            else
            {
                descriptionText += "\n\n<color=#FFFFFF>Pressione <b>ESPAÇO</b> para recomeçar</color>";
            }

            successDescription.text = descriptionText;
        }

        // Esconder botões, usar apenas teclado
        if (nextPhaseButton != null) nextPhaseButton.SetActive(false);
        if (restartButton != null) restartButton.SetActive(false);
    }

    private PhaseData GetNextPhasePreview()
    {
        if (!PhaseManager.Instance.HasNextPhase()) return null;
        
        // Avançar temporariamente para pegar a próxima fase
        PhaseManager.Instance.NextPhase();
        PhaseData nextPhase = PhaseManager.Instance.GetCurrentPhase();
        
        // Voltar para a fase atual
        PhaseManager tempManager = PhaseManager.Instance;
        System.Reflection.FieldInfo field = tempManager.GetType().GetField("currentPhaseIndex", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field != null)
        {
            int currentIndex = (int)field.GetValue(tempManager);
            field.SetValue(tempManager, currentIndex - 1);
        }
        
        return nextPhase;
    }

    public void NextPhase()
    {
        if (PhaseManager.Instance.HasNextPhase())
        {
            PhaseManager.Instance.NextPhase();
            RestartGame();
        }
        else
        {
            // Se não há próxima fase, recomeçar todas
            RestartAllPhases();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void RestartAllPhases()
    {
        PhaseManager.Instance.RestartAllPhases();
        RestartGame();
    }

    public void ShowGameOver(PhaseData phase)
    {
        Time.timeScale = 0;
        GameManager.Instance.EndGame();
        
        if (panelGameOver != null) panelGameOver.SetActive(true);

        if (gameOverTitle != null)
            gameOverTitle.text = "<color=#FF6B6B>GAME OVER!</color>";

        if (gameOverDescription != null)
        {
            gameOverDescription.text = $@"<b>Você pegou a linguagem errada!</b>

<color=#FFFFFF>A   linguagem correta era:</color>
<b><size=32>{phase.linguagem}</size></b>

<color=#FFFFFF>Informações:</color>
{phase.descricao}

<b>Ano de lançamento:</b> {phase.anoDeLancamento}

<color=#FFFFFF>Pressione <b>ESPAÇO</b> para recomeçar esta fase
Pressione <b>S</b> para sair do jogo</color>";
        }

        waitingForInput = true;
    }

    public void ShowTimeOut(PhaseData phase)
    {
        Time.timeScale = 0;
        
        if (panelGameOver != null) panelGameOver.SetActive(true);

        if (gameOverTitle != null)
            gameOverTitle.text = "<color=#FF6B6B>TEMPO ESGOTADO!</color>";

        if (gameOverDescription != null)
        {
            gameOverDescription.text = $@"<b>O tempo acabou!</b>

<color=#FFFFFF>Você estava jogando com:</color>
<b><size=32>{phase.linguagem}</size></b>

<color=#FFFFFF>Informações:</color>
{phase.descricao}

<b>Ano de lançamento:</b> {phase.anoDeLancamento}

<color=#FFFFFF>Pressione <b>ESPAÇO</b> para recomeçar esta fase
Pressione <b>S</b> para sair do jogo</color>";
        }

        waitingForInput = true;
    }

    public void RestartCurrentPhase()
    {
        waitingForInput = false;
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
