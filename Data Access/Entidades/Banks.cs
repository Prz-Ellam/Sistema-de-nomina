using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Banks
    {
        private int bankId;
        private string name;

        public int BankId { get => bankId; set => bankId = value; }
        public string Name { get => name; set => name = value; }
    }
}
