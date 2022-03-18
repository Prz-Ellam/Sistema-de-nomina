using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace Presentation.Helpers
{
    public class RegexUtilities
    {
        bool ValidateName(string name)
        {
            return false;
        }

        bool ValidateCURP(string curp)
        {
            string res = @"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$";
            Regex rx = new Regex(res, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return rx.IsMatch(curp);
        }

        bool ValidateRFC(string rfc)
        {
            string res = @"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$";
            Regex rx = new Regex(res, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return rx.IsMatch(rfc);
        }

        bool ValidateNSS(string nss)
        {
            return false;
        }
    }
}
