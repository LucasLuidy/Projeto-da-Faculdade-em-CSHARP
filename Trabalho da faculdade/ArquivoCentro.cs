using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CentroPesquisa
{
    public static class ArquivoCentro
    {
        private const string NomeArquivo = "centro_pesquisa.json";

        public static Centro CarregarDadosCentro()
        {
            try
            {
                if (File.Exists(NomeArquivo))
                {
                    string json = File.ReadAllText(NomeArquivo);
                    var centro = JsonConvert.DeserializeObject<Centro>(json);
                    if (centro.Pesquisadores == null)
                        centro.Pesquisadores = new List<Pesquisador>();

                    centro.setPesquisadoresArvoreBinaria(CarregarPesquisadoresArvore(json));

                    foreach (var pesquisador in centro.Pesquisadores)
                    {
                        if (centro.BuscarPorMatricula(pesquisador.Matricula) == null)
                        {
                            centro.InserirPesquisador(pesquisador);
                        }
                    }

                    return centro;
                }
            }
            catch (Exception ex)
            {
                InterfaceCentro.ExibirErroCarregarDadosCentro(ex);
            }

            return null;
        }

        public static ArvoreBinaria<Pesquisador>[] CarregarPesquisadoresArvore(string json)
        {
            ArvoreBinaria<Pesquisador>[] arvoreBinarias = null;

            if (!string.IsNullOrEmpty(json))
            {
                var response = JsonConvert.DeserializeObject<dynamic>(json);
                if (response.NomesDepartamentos != null)
                {
                    arvoreBinarias = new ArvoreBinaria<Pesquisador>[response.NomesDepartamentos.Count];

                    for (int i = 0; i < response.NomesDepartamentos.Count; i++)
                    {
                        arvoreBinarias[i] = new ArvoreBinaria<Pesquisador>(x => x.Matricula);
                    }
                }
            }

            return arvoreBinarias;
        }

        public static void GravarDadosCentro(Centro centro)
        {
            try
            {
                string json = JsonConvert.SerializeObject(centro, Formatting.Indented);
                File.WriteAllText(NomeArquivo, json);
                InterfaceCentro.ExibirSucessoGravarDadosCentro();
            }
            catch (Exception ex)
            {
                InterfaceCentro.ExibirErroGravarDadosCentro(ex);
            }
        }

        public static void SalvarPesquisadorEmArquivo(Pesquisador pesquisador, string caminhoArquivo)
        {
            try
            {
                string json = JsonConvert.SerializeObject(pesquisador, Formatting.Indented);
                File.WriteAllText(caminhoArquivo, json);
                InterfaceCentro.ExibirSucessoGravarDadosCentro();
            }
            catch (Exception ex)
            {
                InterfaceCentro.ExibirErroGravarDadosCentro(ex);
            }
        }

        public static Pesquisador CarregarPesquisadorDeArquivo(string caminhoArquivo)
        {
            try
            {
                if (File.Exists(caminhoArquivo))
                {
                    string json = File.ReadAllText(caminhoArquivo);
                    return JsonConvert.DeserializeObject<Pesquisador>(json);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                InterfaceCentro.ExibirErroCarregarDadosCentro(ex);
                return null;
            }
        }
    }
}