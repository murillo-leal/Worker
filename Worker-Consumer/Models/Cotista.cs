using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;
using System.Text.Json.Serialization;

namespace Worker_Consumer.Models
{
    public class Cotista
    {
            
        [Key]
        public int Id { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Cotista")]
        public string CodFundo { get; set; }

        public string ComunicEletr { get; set; }          

            
    }   
        
}
