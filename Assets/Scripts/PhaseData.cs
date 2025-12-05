using System;

[Serializable]
public class PhaseData
{
    public string linguagem;
    public string dica;
    public string descricao;
    public string aplicacao;
    public string curiosidade;
    public int anoDeLancamento;

    public PhaseData(string linguagem, string dica, string descricao, string aplicacao, string curiosidade, int ano)
    {
        this.linguagem = linguagem;
        this.dica = dica;
        this.descricao = descricao;
        this.aplicacao = aplicacao;
        this.curiosidade = curiosidade;
        this.anoDeLancamento = ano;
    }
}
