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
          [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Cotista")]
        public string codFundo { get; set; }

          public string comunicEletr { get; set; }          

        //public Cotista (string codFundo, string comunicEletr)
        //{            
        //    this.codFundo = codFundo;
        //    this.comunicEletr = comunicEletr;
        //}     
    }   
        
}
