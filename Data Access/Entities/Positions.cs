using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class Positions
    {
        int id;
        string name;
        decimal wageLevel;
        bool active;
        int companyId;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public decimal WageLevel { get => wageLevel; set => wageLevel = value; }
        public bool Active { get => active; set => active = value; }
        public int CompanyId { get => companyId; set => companyId = value; }
    }
}
