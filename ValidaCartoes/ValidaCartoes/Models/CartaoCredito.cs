using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ValidaCartoes.Models
{
    public class CartaoCredito
    {
        public string numeroCartao { get; set; }
        public bandeira bandeiraCartao { get; set; }
        public bool valido { get; set; }
        
        public string nomeBandeiraCartao { get { return this.GetEnumDescription(bandeiraCartao); } }
        public string cartaoValido { get { return valido ? "(válido)" : ("inválido"); } }

        /// <summary>
        /// Método GetEnumDescription - Retorna o description de um Enum
        /// </summary>
        /// <param name="valor">Enum</param>
        /// <returns>Retorna string com o description do enum.</returns>
        private string GetEnumDescription(Enum valor)
        {
            FieldInfo fi = valor.GetType().GetField(valor.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return valor.ToString();
        }
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