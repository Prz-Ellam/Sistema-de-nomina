using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public class NumericUtils
    {
        public static string GetNumberString(decimal number)
        {
            string numStr;
            number = Math.Truncate(number);

            if (number == 0) numStr = "CERO";
            else if (number == 1) numStr = "UN";
            else if (number == 2) numStr = "DOS";
            else if (number == 3) numStr = "TRES";
            else if (number == 4) numStr = "CUATRO";
            else if (number == 5) numStr = "CINCO";
            else if (number == 6) numStr = "SEIS";
            else if (number == 7) numStr = "SIETE";
            else if (number == 8) numStr = "OCHO";
            else if (number == 9) numStr = "NUEVE";
            else if (number == 10) numStr = "DIEZ";
            else if (number == 11) numStr = "ONCE";
            else if (number == 12) numStr = "DOCE";
            else if (number == 13) numStr = "TRECE";
            else if (number == 14) numStr = "CATORCE";
            else if (number == 15) numStr = "QUINCE";
            else if (number < 20) numStr = "DIECI" + GetNumberString(number - 10);
            else if (number == 20) numStr = "VEINTE";
            else if (number < 30) numStr = "VEINTI" + GetNumberString(number - 20);
            else if (number == 30) numStr = "TREINTA";
            else if (number == 40) numStr = "CUARENTA";
            else if (number == 50) numStr = "CINCUENTA";
            else if (number == 60) numStr = "SESENTA";
            else if (number == 70) numStr = "SETENTA";
            else if (number == 80) numStr = "OCHENTA";
            else if (number == 90) numStr = "NOVENTA";
            else if (number < 100) numStr = GetNumberString(Math.Truncate(number / 10) * 10) + " Y " + GetNumberString(number % 10);
            else if (number == 100) numStr = "CIEN";
            else if (number < 200) numStr = "CIENTO " + GetNumberString(number - 100);
            else if ((number == 200) || (number == 300) || (number == 400) || (number == 600) || (number == 800)) numStr = GetNumberString(Math.Truncate(number / 100)) + "CIENTOS";
            else if (number == 500) numStr = "QUINIENTOS";
            else if (number == 700) numStr = "SETECIENTOS";
            else if (number == 900) numStr = "NOVECIENTOS";
            else if (number < 1000) numStr = GetNumberString(Math.Truncate(number / 100) * 100) + " " + GetNumberString(number % 100);
            else if (number == 1000) numStr = "MIL";
            else if (number < 2000) numStr = "MIL " + GetNumberString(number % 1000);
            else if (number < 1000000)
            {
                numStr = GetNumberString(Math.Truncate(number / 1000)) + " MIL";
                if ((number % 1000) > 0)
                {
                    numStr = numStr + " " + GetNumberString(number % 1000);
                }
            }
            else if (number == 1000000)
            {
                numStr = "UN MILLON";
            }
            else if (number < 2000000)
            {
                numStr = "UN MILLON " + GetNumberString(number % 1000000);
            }
            else if (number < 1000000000000)
            {
                numStr = GetNumberString(Math.Truncate(number / 1000000)) + " MILLONES ";
                if ((number - Math.Truncate(number / 1000000) * 1000000) > 0)
                {
                    numStr = numStr + " " + GetNumberString(number - Math.Truncate(number / 1000000) * 1000000);
                }
            }
            else if (number == 1000000000000) numStr = "UN BILLON";
            else if (number < 2000000000000) numStr = "UN BILLON " + GetNumberString(number - Math.Truncate(number / 1000000000000) * 1000000000000);
            else
            {
                numStr = GetNumberString(Math.Truncate((number / 1000000000000))) + " BILLONES";
                if ((number - Math.Truncate(number / 1000000000000) * 1000000000000) > 0)
                {
                    numStr = numStr + " " + GetNumberString(number - Math.Truncate(number / 1000000000000) * 1000000000000);
                }
            }
            return numStr;
        }
    }
}
