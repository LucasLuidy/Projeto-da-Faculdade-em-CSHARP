using System;

namespace CentroPesquisa
{
    public static class InterfaceCentro
    {
        public static void ExibirDetalhesPesquisador(Pesquisador pesquisador)
        {
            Console.WriteLine(pesquisador.ToString());
        }

        public static string ObterDetalhesPesquisadorFormatado(Pesquisador pesquisador)
        {
            return $"Matrícula: {pesquisador.Matricula}, Nome: {pesquisador.Nome}, Formação: {pesquisador.Formacao}, Contato: {pesquisador.Contato}";
        }

        public static void ExibirErroCarregarDadosCentro(Exception ex)
        {
            Console.WriteLine($"Erro ao carregar os dados do centro: {ex.Message}");
        }

        public static void ExibirSucessoGravarDadosCentro()
        {
            Console.WriteLine("Dados do centro gravados com sucesso.");
        }

        public static void ExibirErroGravarDadosCentro(Exception ex)
        {
            Console.WriteLine($"Erro ao gravar os dados do centro: {ex.Message}");
        }
    }
}