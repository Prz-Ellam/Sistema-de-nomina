using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class DepartmentsViewModel
    {
        private int id;
        private string name;
        private decimal baseSalary;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public decimal BaseSalary { get => baseSalary; set => baseSalary = value; }
    }
}
