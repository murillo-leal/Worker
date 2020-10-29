using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_Producer.Models
{
    public class FundoInvest {
    
      public Dados opr_rec_inf { get; set; }
      
    }
    public class Dados
    {
        public string codFundo { get; set; }
        public string tpempres { get; set; }
        public string codBanco { get; set; }
        public string agencia { get; set; }
        public string conta { get; set; }
        public string dac10 { get; set; }
        public DateTime data { get; set; }
        public string comunicEletr { get; set; }
        public int codUsuario { get; set; }
        public string opedCana { get; set; }
        public string codCana { get; set; }
        public string tipoMovi { get; set; }
    }


}
