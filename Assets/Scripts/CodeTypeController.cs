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

    [Header("Som de Digitação")]
    [SerializeField] private AudioClip sfxTyping;
    [SerializeField] [Range(0f, 1f)] private float typingVolume = 0.5f;

    private string fullCode;
    private AudioSource _audioSource;

    private void Start()
    {
        Time.timeScale = 0;
        hudPanel.SetActive(false);
        
        SetupAudioSource();
        
        if (PhaseManager.Instance == null)
        {
            GameObject phaseManagerObj = new GameObject("PhaseManager");
            phaseManagerObj.AddComponent<PhaseManager>();
        }
        
        GenerateCodeForCurrentPhase();
        StartCoroutine(TypeCode());
    }

    private void SetupAudioSource()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = sfxTyping;
        _audioSource.loop = true;
        _audioSource.volume = typingVolume;
        _audioSource.playOnAwake = false;
    }

    private void StartTypingSound()
    {
        if (_audioSource != null && sfxTyping != null && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    private void StopTypingSound()
    {
        if (_audioSource != null && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
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

<color=#569CD6>var</color> <color=#9CDCFE>dica</color> = <color=#CE9178>""{currentPhase.dica}""</color>;

<color=#D4D4D4>// Qual linguagem é essa?</color>
<color=#569CD6>if</color> (<color=#9CDCFE>acertou</color>) {{
    <color=#9CDCFE>promocao</color>();
}} <color=#569CD6>else</color> {{
    <color=#9CDCFE>demissao</color>();
}}";
    }

    private IEnumerator TypeCode()
    {
        codeText.text = "";
        int i = 0;

        StartTypingSound();

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

        StopTypingSound();

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
