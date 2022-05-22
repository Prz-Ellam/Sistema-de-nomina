using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class PayrollReportsViewModel
    {
        string department;
        string year;
        string month;
        decimal grossSalary;
        decimal netSalary;

        [Name("Departamento")]
        public string Department { get => department; set => department = value; }

        [Name("Año")]
        public string Year { get => year; set => year = value; }

        [Name("Mes")]
        public string Month { get => month; set => month = value; }

        [Name("Sueldo bruto")]
        [Format("c")]
        public decimal GrossSalary { get => grossSalary; set => grossSalary = value; }

        [Name("Sueldo neto")]
        [Format("c")]
        public decimal NetSalary { get => netSalary; set => netSalary = value; }
    }
}
