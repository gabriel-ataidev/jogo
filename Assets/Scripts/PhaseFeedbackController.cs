using UnityEngine;
using TMPro;

public class PhaseFeedbackManager : MonoBehaviour
{
    public static PhaseFeedbackManager Instance;

    [SerializeField] private GameObject panelErrorJS;
    [SerializeField] private GameObject panelErrorPython;
    [SerializeField] private GameObject panelSuccessCSharp;

    [SerializeField] private TMP_Text jsErrorTitle;
    [SerializeField] private TMP_Text jsErrorDescription;

    [SerializeField] private TMP_Text pyErrorTitle;
    [SerializeField] private TMP_Text pyErrorDescription;

    [SerializeField] private TMP_Text csharpSuccessTitle;
    [SerializeField] private TMP_Text csharpSuccessDescription;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        HideAllPanels();
        SetupTexts();
    }

    void HideAllPanels()
    {
        if (panelErrorJS != null) panelErrorJS.SetActive(false);
        if (panelErrorPython != null) panelErrorPython.SetActive(false);
        if (panelSuccessCSharp != null) panelSuccessCSharp.SetActive(false);
    }

    void SetupTexts()
    {
        if (jsErrorTitle != null)
            jsErrorTitle.text = "GAME OVER!";
        if (jsErrorDescription != null)
            jsErrorDescription.text =
@"<b>Você pegou JavaScript!</b>

<color=#FF6B6B>Por que está errado?</color>
JavaScript não é uma linguagem puramente Orientada a Objetos.

Apesar de suportar classes e objetos, ela é <b>multi-paradigma</b> e também permite programação funcional e procedural.

A missão era pegar apenas linguagens <b>puramente POO</b>!";

        if (pyErrorTitle != null)
            pyErrorTitle.text = "GAME OVER!";
        if (pyErrorDescription != null)
            pyErrorDescription.text =
@"<b>Você pegou Python!</b>

<color=#FF6B6B>Por que está errado?</color>
Python é uma linguagem <b>multi-paradigma</b>.

Embora suporte POO, permite código funcional e procedural. 
Nem tudo em Python é objeto, por isso não se encaixa como puramente Orientada a Objetos!";

        if (csharpSuccessTitle != null)
            csharpSuccessTitle.text = "PARABÉNS!";
        if (csharpSuccessDescription != null)
            csharpSuccessDescription.text =
@"<b>Você coletou 10 linguagens C#!</b>
C# é uma linguagem <b>fortemente Orientada a Objetos</b>.

Ela aplica todos os pilares da POO:
• Encapsulamento  
• Herança  
• Polimorfismo  
• Abstração

Além disso, é amplamente usada na Unity, Web e Desktop.

<color=#FFC107>Missão cumprida, ninja!</color>";
    }

    public void ShowErrorJS()
    {
        Time.timeScale = 0;
        if (panelErrorJS != null) panelErrorJS.SetActive(true);
        GameManager.Instance.EndGame();
    }

    public void ShowErrorPython()
    {
        Time.timeScale = 0;
        if (panelErrorPython != null) panelErrorPython.SetActive(true);
        GameManager.Instance.EndGame();
    }

    public void ShowSuccessCSharp()
    {
        Time.timeScale = 0;
        if (panelSuccessCSharp != null) panelSuccessCSharp.SetActive(true);
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
