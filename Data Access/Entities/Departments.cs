using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Data_Access.Entities
{
    public class Departments
    {
        private int id;
        private string name;
        private decimal baseSalary;
        private bool active;
        private int company_id;

        public int Id { get => id; set => id = value; }

        [Required(ErrorMessage = "Todos los campos son obligatorios")]
        [RegularExpression("^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage ="Nombre de departamento solo puede contener letras")]
        public string Name { get => name; set => name = value; }

        [Required(ErrorMessage = "Todos los campos son obligatorios")]
        public decimal BaseSalary { get => baseSalary; set => baseSalary = value; }
        public bool Active { get => active; set => active = value; }

        [Required(ErrorMessage = "Todos los campos son obligatorios")]
        public int Company_id { get => company_id; set => company_id = value; }
    }
}
