using System.Collections.Generic;

public class Centro
{
    private string[] nomesDepartamentos; 
    private ArvoreBinaria<Pesquisador>[] pesquisadoresPorDepartamento; 

    public Centro(int numeroDepartamentos)
    {
        nomesDepartamentos = new string[numeroDepartamentos];
        pesquisadoresPorDepartamento = new ArvoreBinaria<Pesquisador>[numeroDepartamentos];

        for (int i = 0; i < numeroDepartamentos; i++)
        {
            pesquisadoresPorDepartamento[i] = new ArvoreBinaria<Pesquisador>(p => p.Matricula);
        }
    }

    public bool MatriculaExistente(int matricula)
    {
        foreach (var arvore in pesquisadoresPorDepartamento)
        {
            if (arvore != null && arvore.Pesquisar(matricula) != null)
                return true;
        }

        return false;
    }

    public void setPesquisadoresArvoreBinaria(ArvoreBinaria<Pesquisador>[] arvoreBinarias)
    {
        this.pesquisadoresPorDepartamento = arvoreBinarias;
    }

    public string[] NomesDepartamentos
    {
        get { return nomesDepartamentos; }
        set { nomesDepartamentos = value; }
    }

    public List<Pesquisador> Pesquisadores { get; set; }

    private int Hash(int matricula)
    {
        return matricula % nomesDepartamentos.Length;
    }

    public Pesquisador BuscarPorMatricula(int matricula)
    {
        int indiceDepartamento = Hash(matricula);
        return pesquisadoresPorDepartamento[indiceDepartamento].Pesquisar(matricula);
    }

    public int InserirPesquisador(Pesquisador pesquisador)
    {
        int indiceDepartamento = Hash(pesquisador.Matricula);
        pesquisadoresPorDepartamento[indiceDepartamento].Inserir(pesquisador);
        return indiceDepartamento;
    }

    public Pesquisador RemoverPesquisador(int matricula)
    {
        int indiceDepartamento = Hash(matricula);
        return pesquisadoresPorDepartamento[indiceDepartamento].Remover(matricula);
    }

    public IEnumerable<Pesquisador> VisitarEmLargura(int indiceDepartamento)
    {
        return pesquisadoresPorDepartamento[indiceDepartamento].VisitarEmLargura();
    }

    public bool VisitaEmLarguraEmProgresso(int indiceDepartamento)
    {
        return pesquisadoresPorDepartamento[indiceDepartamento].VisitaEmLarguraEmProgresso();
    }
}