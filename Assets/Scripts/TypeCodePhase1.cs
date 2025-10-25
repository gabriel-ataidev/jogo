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

    private string fullCode =
@"<color=#569CD6>if</color> (<color=#9CDCFE>linguagem</color>.equals(<color=#CE9178>""Orientada_A_Objetos""</color>)) {
    <color=#9CDCFE>pontuacao</color><color=#D4D4D4>++;</color>
} <color=#569CD6>else</color> {
    <color=#9CDCFE>gameOver</color>();
}";

    private void Start()
    {
        Time.timeScale = 0;
        hudPanel.SetActive(false);
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
