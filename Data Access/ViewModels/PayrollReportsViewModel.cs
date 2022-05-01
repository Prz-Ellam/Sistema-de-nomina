using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class PayrollReportsViewModel
    {
        string departamento;
        string anio;
        string mes;
        decimal sueldoBruto;
        decimal sueldoNeto;

        public string Departamento { get => departamento; set => departamento = value; }
        public string Anio { get => anio; set => anio = value; }
        public string Mes { get => mes; set => mes = value; }
        public decimal SueldoBruto { get => sueldoBruto; set => sueldoBruto = value; }
        public decimal SueldoNeto { get => sueldoNeto; set => sueldoNeto = value; }
    }
}
