using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ValidaCartoes.Models
{
    public class CartaoCredito
    {
        public string numeroCartao { get; set; }
        public bandeira bandeiraCartao { get; set; }
        public bool valido { get; set; }
    }

    public enum bandeira {
        [Description("Desconhecido")]
        DESCONHECIDO = 0,
        [Description("VISA")]
        VISA = 1,
        [Description("MasterCard")]
        MASTERCARD = 2,
        [Description("Discover")]
        DISCOVER = 3,
        [Description("AMEX")]
        AMEX = 4
    }
}