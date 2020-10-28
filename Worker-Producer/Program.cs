using Confluent.Kafka;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Worker_Producer.Models;

namespace Worker_Producer
{
    class Program
    {
        public static async Task Main()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using var p = new ProducerBuilder<Null, string>(config).Build();

            {
                try
                {
                    
                    while (true)
                    {
                        var resultA = new FundoInvest() { FundoInvestimentos = "opc-rec-in", Tpempres = "004", CodBanco = "341", Agencia = "1403", Conta = "12222222", Dac10 = "5", Data = new DateTime(), ComunicEletr = "S", CodUsuario = 0, OpedCana = "00", CodCana = "00", TipoMovi = "N" };
                        var resultB = JsonSerializer.Serialize(resultA);

                        var dr = await p.ProduceAsync("test-topic",
                        new Message<Null, string> { Value = $"{resultB}" });

                        Console.WriteLine($"Delivered '{dr.Value}''");

                        Thread.Sleep(2000);
                    }
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
