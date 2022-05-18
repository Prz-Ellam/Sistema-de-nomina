using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data_Access.Helpers
{
    public class Phone : ValidationAttribute
    {
        
        public override bool IsValid(object phones)
        {
            List<string> listPhones = phones as List<string>;

            if (listPhones == null)
            {
                return false;
            }

            if (listPhones.Count > 10)
            {
                return false;
            }

            string res = @"^\d{10}$";
            Regex rx = new Regex(res, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (var phone in listPhones)
            {
                if (!rx.IsMatch(phone))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
