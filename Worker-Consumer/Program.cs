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
                                        agencia = resultA.opr_rec_inf.agencia,
                                        conta = resultA.opr_rec_inf.conta,
                                        Cotista = new List<Cotista>
                                        {
                                            new Cotista { codFundo = resultA.opr_rec_inf.codFundo, comunicEletr = resultA.opr_rec_inf.comunicEletr },

                                        }
                                    };                            
                                    db.Person.Add(person);
                                    db.SaveChanges();
                                
                                    Console.WriteLine($"'{person.agencia}','{person.conta}','{person.Cotista}'");
                                }

                                                      
;
                                

                                //Cotista cotista = new Cotista(resultA.opr_rec_inf.codFundo, resultA.opr_rec_inf.comunicEletr);
                                //Person pessoa = new Person(resultA.opr_rec_inf.conta, resultA.opr_rec_inf.agencia, resultA.opr_rec_inf.conta, cotista);


                                //Console.WriteLine($"'{pessoa.idCotist}','{pessoa.agencia}','{pessoa.conta}','{pessoa.cotista.codFundo}', '{pessoa.cotista.comunicEletr}'");








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
   

 