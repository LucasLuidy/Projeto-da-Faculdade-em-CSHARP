public class Pesquisador
{
    private int matricula;
    private string nome;
    private string formacao;
    private string contato;
    public int departamento;

    public Pesquisador()
    {
    }

    public Pesquisador(int matricula, string nome, string formacao, string contato)
    {
        this.matricula = matricula;
        this.nome = nome;
        this.formacao = formacao;
        this.contato = contato;
    }

    public int Matricula
    {
        get { return matricula; }
        set { matricula = value; }
    }

    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

    public string Formacao
    {
        get { return formacao; }
        set { formacao = value; }
    }

    public string Contato
    {
        get { return contato; }
        set { contato = value; }
    }
  
    public override string ToString()
    {
         return CentroPesquisa.InterfaceCentro.ObterDetalhesPesquisadorFormatado(this);
    }
}
