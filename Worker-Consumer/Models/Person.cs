using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using static Worker_Consumer.Models.Cotista;

namespace Worker_Consumer.Models
{
    public class Person
    {
        [Key]
        public string idCotist { get; set; }

        [JsonPropertyName("agencia")]
        public string agencia { get; set; }

        [JsonPropertyName("conta")]
        public string conta { get; set; }
        public Cotista cotista { get; set; }


        public Person (string idCotist, string agencia, string conta, Cotista cotista)
        {
            this.idCotist = idCotist;
            this.agencia = agencia;
            this.conta = conta;
            this.cotista = cotista;

        }

    }
        
}
