using System;

[Serializable]
public class PhaseData
{
    public string linguagem;
    public string descricao;
    public int anoDeLancamento;

    public PhaseData(string linguagem, string descricao, int ano)
    {
        this.linguagem = linguagem;
        this.descricao = descricao;
        this.anoDeLancamento = ano;
    }
}
