using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.Json.Serialization;

namespace Worker_Consumer.Models
{
    public class Cotista
    {
        
            [JsonPropertyName("codFundo")]
            public string codFundo { get; set; }

            [JsonPropertyName("comunicEletr")]
            public string comunicEletr { get; set; }

        public Cotista (string codFundo, string comunicEletr)
        {
            this.codFundo = codFundo;
            this.comunicEletr = comunicEletr;
        }
        
    }
}
