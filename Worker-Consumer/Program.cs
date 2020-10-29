using Confluent.Kafka;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Worker_Consumer.Models;
using Worker_Producer.Models;

namespace Worker_Consumer
{
    class Program
    {
        public static void Main(string[] args)
        {
            //JSON configuration
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                AllowTrailingCommas = true
            };

            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "127.0.0.1:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
            {
                c.Subscribe("test-topic");

                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {

                            var cr = c.Consume(cts.Token);
                            // Console.WriteLine($"Consumed message '{cr.Message.Value}'");

                            string jsonString = cr.Message.Value;
                            var resultA = JsonSerializer.Deserialize<FundoInvest>(jsonString, options);
                            //   var resultB = JsonSerializer.Serialize(resultA, options);
                            var test = JsonSerializer.Deserialize<FundoInvest>(jsonString, options);



                            Cotista cotista = new Cotista(resultA.opr_rec_inf.codFundo, resultA.opr_rec_inf.comunicEletr);
                            Person pessoa = new Person(resultA.opr_rec_inf.conta, resultA.opr_rec_inf.agencia, resultA.opr_rec_inf.conta, cotista);
                            
                                                        
                            Console.WriteLine($"'{pessoa.idCotist}','{pessoa.agencia}','{pessoa.conta}','{pessoa.cotista.codFundo}', '{pessoa.cotista.comunicEletr}'");





                            


                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }
            }
        }
    }
}
