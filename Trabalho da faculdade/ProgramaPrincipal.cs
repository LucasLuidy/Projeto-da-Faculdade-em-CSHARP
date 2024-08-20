using System;
using System.Linq;
using System.Collections.Generic;

namespace CentroPesquisa
{
    class ProgramaPrincipal
    {
        static void Main()
        {
            Centro centro = ArquivoCentro.CarregarDadosCentro();

            if (centro == null || centro.NomesDepartamentos.Length == 0)
            {
               Console.Write("Informe o número de departamentos: ");
              int numeroDepartamentos;
              while (!int.TryParse(Console.ReadLine(), out numeroDepartamentos) || numeroDepartamentos <= 0)
              {
                    Console.WriteLine("Entrada inválida. Por favor, digite um número de departamentos valido.");
                  Console.Write("Informe o número de departamentos novamente: ");
                }

                centro = new Centro(numeroDepartamentos);

                for (int i = 0; i < numeroDepartamentos; i++)
                {
                    string nomeDepartamento = "";
                    while (string.IsNullOrWhiteSpace(nomeDepartamento))
                    {
                        Console.Write($"Informe o nome do departamento {i + 1}: ");
                        nomeDepartamento = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nomeDepartamento))
                        {
                            Console.WriteLine("O nome do departamento não pode ser vazio. Por favor, digite novamente.");
                        }
                    }
                    centro.NomesDepartamentos[i] = nomeDepartamento;
                }
            }

            int opcao;

            while (true)
            {
                Console.WriteLine("\n===== Menu Principal =====");
                Console.WriteLine("1. Inserir Pesquisador");
                Console.WriteLine("2. Listar Departamentos");
                Console.WriteLine("3. Listar Pesquisadores de um Departamento");
                Console.WriteLine("4. Buscar Pesquisador");
                Console.WriteLine("5. Gravar");
                Console.WriteLine("6. Sair");

                Console.Write("Escolha uma opção (1-6): ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out opcao) || opcao < 1 || opcao > 6)
                {
                    Console.WriteLine("Opção inválida. Por favor, digite um número válido (1-6).");
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        InserirPesquisador(centro);
                        break;
                    case 2:
                        ListarDepartamentos(centro);
                        break;
                    case 3:
                        ListarPesquisadoresDepartamento(centro);
                        break;
                    case 4:
                        BuscarPesquisador(centro);
                        break;
                    case 5:
                        GravarDados(centro);
                        break;
                    case 6:
                        Sair(centro);
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void InserirPesquisador(Centro centro)
        {
            Console.WriteLine("\n===== Inserir Pesquisador =====");

            int matricula = 0;
            bool entradaValida = false;

            while (!entradaValida)
            {
                Console.Write("Matrícula: ");
                if (int.TryParse(Console.ReadLine(), out matricula))
                {
                    entradaValida = true;
                }
                else
                {
                    Console.WriteLine("Matrícula inválida. Por favor, digite um número.");
                }
            }

            if (centro.BuscarPorMatricula(matricula) != null)
            {
                Console.WriteLine("Erro: Matrícula já utilizada. Pesquisador existente:");
                Console.WriteLine(centro.BuscarPorMatricula(matricula));
                return;
            }

          string nome = string.Empty;
          while (true)
          {
              Console.Write("Nome: ");
              nome = Console.ReadLine();

              if (string.IsNullOrWhiteSpace(nome))
              {
                  Console.WriteLine("Nome não pode ser vazio. Por favor, digite um nome válido.");
              }
              else if (nome.Any(char.IsDigit))
              {
                  Console.WriteLine("Nome não pode conter números. Por favor, digite um nome válido.");
                  nome = string.Empty;
              }
              else if (!nome.Replace(" ", "").All(char.IsLetter))
              {
                  Console.WriteLine("Nome não pode conter caracteres especiais. Por favor, digite um nome válido.");
                  nome = string.Empty;
              }
              else
              {
                  break;
              }
          }

          string formacao = string.Empty;
          while (true)
          {
              Console.Write("Formação: ");
              formacao = Console.ReadLine();

              if (string.IsNullOrWhiteSpace(formacao))
              {
                  Console.WriteLine("Formação não pode ser vazia. Por favor, digite uma formação válida.");
              }
              else if (formacao.Any(char.IsDigit))
              {
                  Console.WriteLine("Formação não pode conter números. Por favor, digite uma formação válida.");
                  formacao = string.Empty;
              }
              else if (!formacao.Replace(" ", "").All(char.IsLetter))
              {
                  Console.WriteLine("Formação não pode conter caracteres especiais. Por favor, digite uma formação válida.");
                  formacao = string.Empty;
              }
              else
              {
                  break;
              }
          }

            string contato = string.Empty;
            while (string.IsNullOrWhiteSpace(contato))
            {
                Console.Write("Contato: ");
                contato = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(contato))
                {
                    Console.WriteLine("Contato não pode ser vazio. Por favor, digite um contato válido.");
                }
            }

            Pesquisador novoPesquisador = new Pesquisador(matricula, nome, formacao, contato);

            int departamento = centro.InserirPesquisador(novoPesquisador);
            novoPesquisador.departamento = departamento;

            if (centro.Pesquisadores == null)
                centro.Pesquisadores = new List<Pesquisador>();
            centro.Pesquisadores.Add(novoPesquisador);

            Console.WriteLine("Pesquisador inserido com sucesso.");
        }

        static void ListarDepartamentos(Centro centro)
        {
            Console.WriteLine("\n===== Listar Departamentos =====");
            string[] nomesDepartamentos = centro.NomesDepartamentos;

            for (int i = 0; i < nomesDepartamentos.Length; i++)
            {
                Console.WriteLine($"{i + 0}. {nomesDepartamentos[i]}");
            }
        }

        static void ListarPesquisadoresDepartamento(Centro centro)
        {
            Console.WriteLine("\n===== Listar Pesquisadores de um Departamento =====");

            int indiceDepartamento;
            string nomeDepartamento;
            string escolha;

            do
            {
                Console.WriteLine("Você deseja pesquisar por índice ou nome do departamento?");
                Console.WriteLine("1. Por índice");
                Console.WriteLine("2. Por nome");
                Console.Write("Escolha uma opção (1-2): ");
                escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        bool entradaValida = false;
                        while (!entradaValida)
                        {
                            Console.Write("Informe o código (índice) do departamento: ");
                            if (int.TryParse(Console.ReadLine(), out indiceDepartamento))
                            {
                                if (indiceDepartamento >= 0 && indiceDepartamento < centro.NomesDepartamentos.Length)
                                {
                                    ListarPesquisadoresPorDepartamento(centro, indiceDepartamento);
                                    return; 
                                }
                                else
                                {
                                    Console.WriteLine("Código de departamento inválido.");
                                }
                                entradaValida = true;
                            }
                            else
                            {
                                Console.WriteLine("Entrada inválida. O índice deve ser um número.");
                            }
                        }
                        break;
                    case "2":
                    do
                    {
                        Console.Write("Informe o nome do departamento: ");
                        nomeDepartamento = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(nomeDepartamento))
                        {
                            Console.WriteLine("O nome do departamento não pode ser vazio. Por favor, digite novamente.");
                        }
                    } while (string.IsNullOrWhiteSpace(nomeDepartamento));

                    int index = Array.IndexOf(centro.NomesDepartamentos, nomeDepartamento);
                    if (index != -1)
                    {
                        ListarPesquisadoresPorDepartamento(centro, index);
                        return; 
                    }
                    else
                    {
                        Console.WriteLine("Departamento não encontrado.");
                        return; 
                    }

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            } while (true); 
        }

        static void ListarPesquisadoresPorDepartamento(Centro centro, int indiceDepartamento)
        {
            var pesquisadores = centro.Pesquisadores.Where(x => x.departamento == indiceDepartamento).ToList();

            if (pesquisadores.Count > 0)
            {
                Console.WriteLine($"\nPesquisadores do departamento {centro.NomesDepartamentos[indiceDepartamento]}:");
                foreach (var pesquisador in pesquisadores)
                {
                    Console.WriteLine(pesquisador);
                }
            }
            else
            {
                Console.WriteLine("Não há pesquisadores neste departamento.");
            }
        }

        static void BuscarPesquisador(Centro centro)
        {
            Console.WriteLine("\n===== Buscar Pesquisador =====");

            int matricula = 0;
            bool entradaValida = false;

            while (!entradaValida)
            {
                Console.Write("Informe a matrícula do pesquisador: ");
                if (int.TryParse(Console.ReadLine(), out matricula))
                {
                    entradaValida = true;
                }
                else
                {
                    Console.WriteLine("Precisa ser um número! Informe a matrícula novamente:");
                }
            }

            Pesquisador pesquisadorEncontrado = centro.Pesquisadores.Where(x => x.Matricula == matricula).FirstOrDefault();

            if (pesquisadorEncontrado != null)
            {
                Console.WriteLine($"Pesquisador encontrado:\n{pesquisadorEncontrado}");

                bool respostaValida = false;
                while (!respostaValida)
                {
                    Console.WriteLine("Deseja remover este pesquisador?");
                    Console.WriteLine("(S) Sim.");
                    Console.WriteLine("(N) Não,retornar ao Menu Principal.");
                    Console.Write("Escolha uma opção (S/N): ");
                    string resposta = Console.ReadLine().ToUpper();

                    if (resposta == "S")
                    {
                        centro.RemoverPesquisador(matricula);
                        centro.Pesquisadores.Remove(centro.Pesquisadores.Where(x => x.Matricula == matricula).FirstOrDefault());
                        Console.WriteLine("Pesquisador removido com sucesso.");
                        respostaValida = true;
                    }
                    else if (resposta == "N")
                    {
                        respostaValida = true;
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida. Por favor, digite 'S' para remover ou 'N' para retornar ao Menu Principal.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Pesquisador não encontrado.");
            }
        }

        static void GravarDados(Centro centro)
        {
            Console.WriteLine("\n===== Gravar Dados =====");
            ArquivoCentro.GravarDadosCentro(centro);
        }

        static void Sair(Centro centro)
        {
            Console.WriteLine("\n===== Sair =====");
            Console.WriteLine("(S) Sim.");
            Console.WriteLine("(N) Não.");
            Console.Write("Deseja gravar os dados antes de sair? (S/N): ");
            string resposta = Console.ReadLine().ToUpper();

            while (resposta != "S" && resposta != "N")
            {
                Console.Write("Por favor, digite 'S' para gravar os dados ou 'N' para não gravar os dados antes de sair: ");
                resposta = Console.ReadLine().ToUpper();
            }

            if (resposta == "S")
            {
                ArquivoCentro.GravarDadosCentro(centro);
            }

            Console.WriteLine("Programa encerrado.");
            Environment.Exit(0);
        }
    }
}