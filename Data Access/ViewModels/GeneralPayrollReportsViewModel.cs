using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class GeneralPayrollReportsViewModel
    {
        private string department;
        private string position;
        private string employeeName;
        private DateTime startDate;
        private uint age;
        private decimal baseSalary;

        [Name("Departamento")]
        public string Department { get => department; set => department = value; }

        [Name("Puesto")]
        public string Position { get => position; set => position = value; }

        [Name("Nombre de empleado")]
        public string EmployeeName { get => employeeName; set => employeeName = value; }

        [Name("Fecha de ingreso")]
        [Format("dd/MM/yyyy")]
        public DateTime StartDate { get => startDate; set => startDate = value; }

        [Name("Edad")]
        public uint Age { get => age; set => age = value; }

        [Name("Sueldo base")]
        [Format("c")]
        public decimal BaseSalary { get => baseSalary; set => baseSalary = value; }
    }
}
