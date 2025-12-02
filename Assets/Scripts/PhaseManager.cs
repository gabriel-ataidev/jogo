using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance;

    private List<PhaseData> allPhases;
    private List<PhaseData> shuffledPhases;
    private int currentPhaseIndex;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePhases();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializePhases()
    {
        allPhases = new List<PhaseData>
        {
            new PhaseData("Python", "Com sua tipagem dinâmica, é amplamente utilizada no campo de Data Science e Machine Learning", 1991),
            new PhaseData("JavaScript", "É capaz de ser tanto uma linguagem interpretada para o navegador quanto uma linguagem de servidor", 1995),
            new PhaseData("C#", "Linguagem orientada a objetos, criada pela Microsoft como parte da plataforma .NET", 2000),
            new PhaseData("Go", "Criada pelo Google para executar muitas tarefas ao mesmo tempo de forma eficiente (concorrência)", 2009),
            new PhaseData("Swift", "Linguagem principal da Apple para desenvolver aplicativos nativos de alta performance para iOS e macOS", 2014)
        };

        ShufflePhases();
    }

    void ShufflePhases()
    {
        shuffledPhases = allPhases.OrderBy(_ => Random.value).ToList();
        currentPhaseIndex = 0;
    }

    public PhaseData GetCurrentPhase()
    {
        if (shuffledPhases == null || shuffledPhases.Count == 0)
        {
            InitializePhases();
        }

        return shuffledPhases != null ? shuffledPhases[currentPhaseIndex] : null;
    }

    public int GetCurrentPhaseNumber()
    {
        return currentPhaseIndex + 1;
    }

    public int GetTotalPhases()
    {
        return shuffledPhases != null ? shuffledPhases.Count : 0;
    }

    public bool HasNextPhase()
    {
        return shuffledPhases != null && currentPhaseIndex < shuffledPhases.Count - 1;
    }

    public void NextPhase()
    {
        if (HasNextPhase())
        {
            currentPhaseIndex++;
        }
    }

    public void RestartAllPhases()
    {
        ShufflePhases();
    }

    public bool IsLastPhase()
    {
        return shuffledPhases != null && currentPhaseIndex == shuffledPhases.Count - 1;
    }

    public PhaseData GetPhaseAtIndex(int index)
    {
        if (shuffledPhases == null || index < 0 || index >= shuffledPhases.Count)
            return null;

        return shuffledPhases[index];
    }
}
