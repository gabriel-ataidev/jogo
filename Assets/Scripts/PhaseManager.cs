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
            new PhaseData(
                "Python",
                "Usa indentação como parte da sintaxe - nada de chaves {} aqui!",
                "Interpretada e de alto nível, com tipagem dinâmica. Foco em legibilidade e rapidez de desenvolvimento.",
                "Data Science, IA e Automação",
                "O nome é uma homenagem ao grupo de comédia britânico Monty Python, e não à cobra!",
                1991
            ),
            new PhaseData(
                "JavaScript",
                "A única linguagem que roda nativamente em todos os navegadores!",
                "Linguagem interpretada, multiparadigma, com tipagem dinâmica e event-driven.",
                "Web Frontend, Mobile, Backend",
                "Foi criada por Brendan Eich em apenas 10 dias, e seu nome original era Mocha.",
                1995
            ),
            new PhaseData(
                "C#",
                "É a estrela do ecossistema .NET, concebida pela Microsoft como uma resposta ao Java.",
                "Compilada, Orientada a Objetos, com tipagem estática e forte. Tem Garbage Collector.",
                "Games (como esse aqui), Desktop Windows, Backend .NET",
                "O símbolo # representa quatro sinais de + (C++++), indicando uma evolução do C++.",
                2000
            ),
            new PhaseData(
                "Go",
                "Sua concorrência é leve e eficiente, usando rotinas para gerenciar milhares de threads simultaneamente.",
                "Compilada e estaticamente tipada, focada em simplicidade e alta performance.",
                "Infra, Cloud e Microservices (Docker, Kubernetes)",
                "O projeto sigiloso foi iniciado dentro do Google por lendas da computação, incluindo Ken Thompson (criador do UNIX e C).",
                2009
            ),
            new PhaseData(
                "Swift",
                "Seus Optionals eliminam erros de ponteiro nulo (ou poderia dizer nil) - segurança em primeiro lugar!",
                "Moderna, de código aberto, com tipagem forte e focada em performance e sintaxe limpa.",
                "Apps iOS, macOS, watchOS e tvOS",
                "Foi apresentada ao público em 2014, mas estava em desenvolvimento às escondidas na Apple desde 2010 para substituir Objective-C.",
                2014
            )
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
