using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text codeText;
    [SerializeField] private GameObject codePanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private float displayTime = 1f;

    private string fullCode;

    private void Start()
    {
        Time.timeScale = 0;
        hudPanel.SetActive(false);
        
        // Garantir que o PhaseManager existe
        if (PhaseManager.Instance == null)
        {
            GameObject phaseManagerObj = new GameObject("PhaseManager");
            phaseManagerObj.AddComponent<PhaseManager>();
        }
        
        GenerateCodeForCurrentPhase();
        StartCoroutine(TypeCode());
    }

    private void GenerateCodeForCurrentPhase()
    {
        if (PhaseManager.Instance == null)
        {
            Debug.LogError("PhaseManager.Instance é null!");
            fullCode = "// Erro: PhaseManager não inicializado";
            return;
        }

        PhaseData currentPhase = PhaseManager.Instance.GetCurrentPhase();
        
        if (currentPhase == null)
        {
            Debug.LogError("currentPhase é null!");
            fullCode = "// Erro: Fase atual não encontrada";
            return;
        }
        
        fullCode = $@"<color=#D4D4D4>// Fase {PhaseManager.Instance.GetCurrentPhaseNumber()}/{PhaseManager.Instance.GetTotalPhases()}</color>

<color=#569CD6>var</color> <color=#9CDCFE>linguagem</color> = <color=#CE9178>""{currentPhase.linguagem}""</color>;
<color=#569CD6>var</color> <color=#9CDCFE>descricao</color> = <color=#CE9178>""{currentPhase.descricao}""</color>;
<color=#569CD6>var</color> <color=#9CDCFE>ano</color> = <color=#B5CEA8>{currentPhase.anoDeLancamento}</color>;

<color=#569CD6>if</color> (<color=#9CDCFE>acertou</color>) {{
    <color=#9CDCFE>proximaFase</color>();
}} <color=#569CD6>else</color> {{
    <color=#9CDCFE>gameOver</color>();
}}";
    }

    private IEnumerator TypeCode()
    {
        codeText.text = "";
        int i = 0;

        while (i < fullCode.Length)
        {
            if (fullCode[i] == '<')
            {
                int closeIndex = fullCode.IndexOf('>', i);
                if (closeIndex != -1)
                {
                    codeText.text += fullCode.Substring(i, closeIndex - i + 1);
                    i = closeIndex + 1;
                    continue;
                }
            }

            codeText.text += fullCode[i];
            i++;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        yield return new WaitForSecondsRealtime(displayTime);
        codeText.text += "\n\n<color=#D4D4D4>Pressione <b>Espaço</b> para começar...</color>";
        yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame);

        Time.timeScale = 1;
        hudPanel.SetActive(true);
        codePanel.SetActive(false);
        codeText.gameObject.SetActive(false);
        GameManager.Instance.StartTimer();
    }
}
