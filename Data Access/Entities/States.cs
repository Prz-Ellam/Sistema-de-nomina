using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class States
    {
        public string state { get; set; }
        public List<string> cities { get; set; }

        public States()
        {
            cities = new List<string>();
        }
    }
}
