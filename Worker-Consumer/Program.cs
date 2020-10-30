using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
            //KAFKA configuration
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "127.0.0.1:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };


            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
                {
                    c.Subscribe("test-topic");


                    try
                    {
                        while (true)
                        {
                            try
                            {

                                var cr = c.Consume(cts.Token);
                              
                                //Deserializing JSON message
                                string jsonString = cr.Message.Value;
                                var resultA = JsonSerializer.Deserialize<FundoInvest>(jsonString, options);
                                                                
                                //Creating NEW object into database
                                using (var db = new PersonDBContext())
                                {
                                    var person = new Person
                                    {
                                        Agencia = resultA.opr_rec_inf.agencia,
                                        Conta = resultA.opr_rec_inf.conta,
                                        Cotista = new List<Cotista>
                                        {
                                            new Cotista { CodFundo = resultA.opr_rec_inf.codFundo, ComunicEletr = resultA.opr_rec_inf.comunicEletr },

                                        }
                                    };                            
                                    db.Person.Add(person);
                                    db.SaveChanges();
                                
                                    Console.WriteLine($"'{person.Agencia}','{person.Conta}','{person.Cotista}'");
                                };
                                

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
            catch (ConsumeException e)
            {

                Console.WriteLine($"Error occured: {e.Error.Reason}");
            }
        }
    }  
}
   

 