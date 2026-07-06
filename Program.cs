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
        static void CalcularViagem()
        {
            Console.Clear();
            Console.WriteLine("--- Nova Simulação de Viagem ---");

            Console.Write("Digite o destino da viagem: ");
            string destino = Console.ReadLine();

            Console.Write("Distância a ser percorrida (em km): ");
            if (!double.TryParse(Console.ReadLine(), out double distancia) || distancia <= 0)
            {
                MostrarErro("Distância inválida.");
                return;
            }

            Console.Write("Consumo médio do veículo (km por litro): ");
            if (!double.TryParse(Console.ReadLine(), out double consumo) || consumo <= 0)
            {
                MostrarErro("Consumo inválido.");
                return;
            }

            Console.Write("Preço do litro do combustível (ex: 5,79): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal preco) || preco <= 0)
            {
                MostrarErro("Preço inválido.");
                return;
            }

            // Cria o objeto e calcula automaticamente pelas propriedades
            var novaViagem = new RelatorioViagem(destino, distancia, consumo, preco);
            _historico.Add(novaViagem);

            Console.WriteLine("\n=== RESULTADO DA SIMULAÇÃO ===");
            Console.WriteLine($"Total de combustível necessário: {novaViagem.TotalLitros:F2} Litros");
            Console.WriteLine($"Custo estimado do combustível: R$ {novaViagem.CustoTotal:F2}");
            Console.WriteLine("\nRelatório salvo no histórico com sucesso!");

            Console.ReadKey();
        }
        static void ExibirHistorico()
        {
            Console.Clear();
            Console.WriteLine("--- Histórico de Viagens Calculadas ---");

            if (!_historico.Any())
            {
                Console.WriteLine("Nenhuma simulação realizada ainda.");
            }
            else
            {
                foreach (var v in _historico)
                {
                    Console.WriteLine($"Destino: {v.Destino} | {v.Distancia}km | Custo: R$ {v.CustoTotal:F2}");
                }

                // Exibe uma métrica geral usando LINQ (Soma total acumulada)
                decimal custoAcumulado = _historico.Sum(v => v.CustoTotal);
                Console.WriteLine("\n-------------------------------------------");
                Console.WriteLine($"Custo total de todas as viagens simuladas: R$ {custoAcumulado:F2}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar.");
            Console.ReadKey();
        }

        static void MostrarErro(string mensagem)
        {
            Console.WriteLine($"\n[ERRO] {mensagem} A operação foi cancelada.");
            Console.ReadKey();
        }
    }
}
    