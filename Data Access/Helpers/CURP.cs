using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data_Access.Helpers
{
    public class CURP : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string strValue = value as string;
            string regexp = @"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$";

            Regex rx = new Regex(regexp, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Match match = rx.Match(strValue);      

            if (!match.Success)
            {
                return new ValidationResult("El curp que ingresó no es válido");
            }

            int digit;
            bool success = int.TryParse(match.Groups[2].Value, out digit);

            if (!success)
            {
                return new ValidationResult("El curp que ingresó no es válido");
            }

            if (digit != DigitoVerificador(match.Groups[1].Value))
            {
                return new ValidationResult("El curp que ingresó no es válido");
            }

            return ValidationResult.Success;
        }

        private decimal DigitoVerificador(string curp)
        {
            string diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            int lngSuma = 0;
            int lngDigito = 0;

            for (var i = 0; i < 17; i++)
            {
                lngSuma = lngSuma + diccionario.IndexOf(curp.ElementAt(i)) * (18 - i);
            }

            lngDigito = 10 - lngSuma % 10;
            if (lngDigito == 10) 
            {
                return 0;
            }

            return lngDigito;
        }
    }
}
