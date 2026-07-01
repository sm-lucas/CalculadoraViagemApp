using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculadoraViagemApp
{

    // Modelo para armazenar o histórico de viagens
    public class RelatorioViagem
    {
        public string Destino { get; set; }
        public double Distancia { get; set; }
        public double ConsumoMedio { get; set; }
        public decimal PrecoCombustivel { get; set; }

        // Propriedades calculadas (Regra de Negócio)
        public double TotalLitros => Distancia / ConsumoMedio;
        public decimal CustoTotal => (decimal)TotalLitros * PrecoCombustivel;

        public RelatorioViagem(string destino, double distancia, double consumoMedio, decimal precoCombustivel)
        {
            Destino = destino;
            Distancia = distancia;
            ConsumoMedio = consumoMedio;
            PrecoCombustivel = precoCombustivel;
        }
    }
    class Program
    {
        private static List<RelatorioViagem> _historico = new List<RelatorioViagem>();

        static void Main(string[] args)
        {
            bool executando = true;

            while (executando)
            {
                Console.Clear();
                Console.WriteLine("=== CALCULADORA DE CUSTO DE VIAGEM ===");
                Console.WriteLine("1 - Calcular Nova Viagem");
                Console.WriteLine("2 - Ver Histórico de Relatórios");
                Console.WriteLine("3 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CalcularViagem();
                        break;
                    case "2":
                        ExibirHistorico();
                        break;
                    case "3":
                        executando = false;
                        Console.WriteLine("Obrigado por usar a calculadora!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Pressione qualquer tecla.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }