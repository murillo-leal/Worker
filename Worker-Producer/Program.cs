using Confluent.Kafka;
using System;
using System.Linq;
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
            //Random string
            static string RandomString(int length)
            {
                Random random = new Random();
                const string chars = "1234567890";
                return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            };

            static string RandomComunic(int length)
            {
                Random random = new Random();
                const string chars = "SN";
                return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }

            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using var p = new ProducerBuilder<Null, string>(config).Build();

            //Configuração JSON Serializer
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                AllowTrailingCommas = true
            };

            {
                try
                {
                    
                    while (true)
                    {

                        
                        var jsonObject = new FundoInvest() { opr_rec_inf = new Dados {codFundo = RandomString(3), tpempres = "004", codBanco = "341", agencia = RandomString(4), conta = RandomString(8), dac10 = "5", data = new DateTime(), comunicEletr = RandomComunic(1), codUsuario = 0, opedCana = "00", codCana = "00", tipoMovi = "N" } };
                        var jsonString = JsonSerializer.Serialize(jsonObject, options);

                        var dr = await p.ProduceAsync("test-topic",
                        new Message<Null, string> { Value = $"{jsonString}" });

                        Console.WriteLine($"'{dr.Value}'");

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
