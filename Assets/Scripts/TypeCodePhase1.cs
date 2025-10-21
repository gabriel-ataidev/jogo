using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    [Header("Referências - Textos")]
    [SerializeField] private TMP_Text codeText;      // texto do código
    [SerializeField] private TMP_Text timerText;     // texto do timer (antes era o título)
    [SerializeField] private TMP_Text scoreText;     // texto da pontuação

    [Header("Referências - Painéis")]
    [SerializeField] private GameObject codePanel;   // painel de fundo do código
    [SerializeField] private GameObject hudPanel;    // novo painel da fase/pontuação

    [Header("Configurações")]
    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private float displayTime = 3f; // tempo antes de trocar HUD
    [SerializeField] private int tempoInicial = 60;  // tempo inicial do timer (segundos)

    private int pontuacao = 0;
    private bool contando = false;

    private string fullCode =
@"<color=#569CD6>if</color> (<color=#9CDCFE>linguagem</color>.equals(<color=#CE9178>""Orientada_A_Objetos""</color>)) {
    <color=#9CDCFE>pontuacao</color><color=#D4D4D4>++;</color>
} <color=#569CD6>else</color> {
    <color=#9CDCFE>gameOver</color>();
}";

    private void Start()
    {
        hudPanel.SetActive(false);
        timerText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        StartCoroutine(TypeCode());

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
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(displayTime);

        ShowPhaseHUD();
    }

    private void ShowPhaseHUD()
    {
        codeText.gameObject.SetActive(false);
        codePanel.SetActive(false);

        hudPanel.SetActive(true);
        timerText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);

        StartCoroutine(TimerContagem());
        AtualizarPontuacao();
    }

    private IEnumerator TimerContagem()
    {
        int tempoRestante = tempoInicial;
        contando = true;

        while (tempoRestante >= 0)
        {
            timerText.text = $"{tempoRestante}s";
            yield return new WaitForSeconds(1f);
            tempoRestante--;
        }

        contando = false;
        timerText.text = "⏱️ Tempo Esgotado!";
    }

    public void AdicionarPonto()
    {
        pontuacao++;
        AtualizarPontuacao();
    }

    private void AtualizarPontuacao()
    {
        scoreText.text = pontuacao.ToString();
    }
}
