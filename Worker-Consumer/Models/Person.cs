using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using static Worker_Consumer.Models.Cotista;

namespace Worker_Consumer.Models
{
    public class Person
    {
        [Key]
        public int PersonID  { get; set; }
        
        public string agencia { get; set; }

        public string conta { get; set; }
             
        public  List<Cotista> Cotista { get; set; }

        //public Person (string agencia, string conta, Cotista Cotista)
        //{
        //    this.agencia = agencia;
        //    this.conta = conta;
        //    _Cotista = Cotista;
        //}

    }
        
}
