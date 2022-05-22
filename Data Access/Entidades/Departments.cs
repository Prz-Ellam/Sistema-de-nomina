using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data_Access.Entidades
{
    public class Departments
    {
        private int departmentId;
        private string name;
        private decimal baseSalary;
        private int companyId;

        public int DepartmentId 
        { 
            get => departmentId;
            set => departmentId = value; 
        }

        [Required(ErrorMessage = "El nombre del departamento es requerido")]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage = "El nombre del departamento solo puede contener letras y espacios")]
        [MaxLength(30, ErrorMessage = "El nombre del departamento no puede tener más de 30 caracteres")]
        public string Name
        { 
            get => name;
            set => name = value; 
        }

        [Required(ErrorMessage = "El sueldo base del departamento es requerido")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "El sueldo base del departamento no puede ser 0")]
        public decimal BaseSalary 
        { 
            get => baseSalary; 
            set => baseSalary = value; 
        }

        [Required(ErrorMessage = "El departamento debe pertenecer a una empresa")]
        public int CompanyId 
        { 
            get => companyId;
            set => companyId = value; 
        }
    }
}
