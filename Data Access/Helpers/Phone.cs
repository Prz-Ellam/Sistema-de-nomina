using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Helpers
{
    public class Phone : ValidationAttribute
    {
        /*
        public override bool IsValid(List mylist)
        {
            if (mylist == null)
                return false;

            return mylist.Any();
        }*/
    }
}
