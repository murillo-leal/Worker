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
        
        public string Agencia { get; set; }

        public string Conta { get; set; }
             
        public  List<Cotista> Cotista { get; set; }
              

    }
        
}
