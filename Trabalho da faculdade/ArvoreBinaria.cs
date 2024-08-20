using System;
using System.Collections.Generic;

public class ArvoreBinaria<Tipo>
{
    private class No
    {
        public Tipo Dado { get; set; }
        public No Esquerda { get; set; }
        public No Direita { get; set; }

        public No(Tipo dado)
        {
            Dado = dado;
            Esquerda = null;
            Direita = null;
        }
    }

    private No Raiz;
    private readonly Func<Tipo, IComparable> selChave;
    private No Atual;
    private Queue<No> Borda;

    public ArvoreBinaria(Func<Tipo, IComparable> selChave)
    {
        this.selChave = selChave;
        Raiz = null;
        Atual = null;
        Borda = new Queue<No>();
    }

    public void EsvaziarArvore()
    {
        Raiz = null;
        Atual = null;
        Borda.Clear();
    }

    public void Inserir(Tipo dado)
    {
        Raiz = InserirRecursivo(Raiz, dado);
    }

    private No InserirRecursivo(No no, Tipo dado)
    {
        if (no == null)
        {
            return new No(dado);
        }

        if (selChave(dado).CompareTo(selChave(no.Dado)) < 0)
        {
            no.Esquerda = InserirRecursivo(no.Esquerda, dado);
        }
        else if (selChave(dado).CompareTo(selChave(no.Dado)) > 0)
        {
            no.Direita = InserirRecursivo(no.Direita, dado);
        }

        return no;
    }

    public Tipo Pesquisar(int chave)
    {
        Atual = PesquisarRecursivo(Raiz, chave);
        return (Atual != null) ? Atual.Dado : default(Tipo);
    }

    private No PesquisarRecursivo(No no, IComparable chave)
    {
        if (no == null || chave.CompareTo(selChave(no.Dado)) == 0)
        {
            return no;
        }

        if (chave.CompareTo(selChave(no.Dado)) < 0)
        {
            return PesquisarRecursivo(no.Esquerda, chave);
        }

        return PesquisarRecursivo(no.Direita, chave);
    }

    public Tipo Remover(int chave)
    {
        Tipo dadoRemovido = Pesquisar(chave);
        Raiz = RemoverRecursivo(Raiz, chave);
        return dadoRemovido;
    }

    private No RemoverRecursivo(No no, IComparable chave)
    {
        if (no == null)
        {
            return no;
        }

        if (chave.CompareTo(selChave(no.Dado)) < 0)
        {
            no.Esquerda = RemoverRecursivo(no.Esquerda, chave);
        }
        else if (chave.CompareTo(selChave(no.Dado)) > 0)
        {
            no.Direita = RemoverRecursivo(no.Direita, chave);
        }
        else
        {
            if (no.Esquerda == null)
            {
                return no.Direita;
            }
            else if (no.Direita == null)
            {
                return no.Esquerda;
            }

            no.Dado = MinimoValor(no.Direita);

            no.Direita = RemoverRecursivo(no.Direita, selChave(no.Dado));
        }

        return no;
    }

    private Tipo MinimoValor(No no)
    {
        Tipo minimoValor = no.Dado;
        while (no.Esquerda != null)
        {
            minimoValor = no.Esquerda.Dado;
            no = no.Esquerda;
        }
        return minimoValor;
    }

    public void ReiniciarBuscaLargura()
    {
        Borda.Clear();
        Borda.Enqueue(Raiz);
    }
  
    public bool ArvoreVazia()
    {
        return Raiz == null;
    }

    public Tipo AtualNaBuscaLargura()
    {
        if (Borda.Count == 0)
        {
            Atual = Raiz;
        }
        else
        {
            Atual = Borda.Dequeue();
            if (Atual.Esquerda != null) Borda.Enqueue(Atual.Esquerda);
            if (Atual.Direita != null) Borda.Enqueue(Atual.Direita);
        }

        return (Atual != null) ? Atual.Dado : default(Tipo);
    }

    public IEnumerable<Tipo> VisitarEmLargura()
    {
        return VisitarEmLargura(Raiz);
    }

    private IEnumerable<Tipo> VisitarEmLargura(No raiz)
    {
        Queue<No> fila = new Queue<No>();
        fila.Enqueue(raiz);

        while (fila.Count > 0)
        {
            No no = fila.Dequeue();

            if (no != null && no.Dado != null)
                yield return no.Dado;

            if (no != null && no.Esquerda != null)
                fila.Enqueue(no.Esquerda);

            if (no != null && no.Direita != null)
                fila.Enqueue(no.Direita);
        }
    }

    public bool VisitaEmLarguraEmProgresso()
    {
        return Borda.Count > 0;
    }
}