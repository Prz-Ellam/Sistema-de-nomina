using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class EmployeePayrollsViewModel
    {
        private int numeroEmpleado;
        private string nombreEmpleado;
        private string departamento;
        private string puesto;
        private decimal sueldoDiario;
        private uint diasTrabajados;
        private decimal sueldoBruto;
        private decimal totalPercepciones;
        private decimal totalDeducciones;
        private decimal sueldoNeto;

        public int NumeroEmpleado { get => numeroEmpleado; set => numeroEmpleado = value; }
        public string NombreEmpleado { get => nombreEmpleado; set => nombreEmpleado = value; }
        public string Departamento { get => departamento; set => departamento = value; }
        public string Puesto { get => puesto; set => puesto = value; }
        public decimal SueldoDiario { get => sueldoDiario; set => sueldoDiario = value; }
        public uint DiasTrabajados { get => diasTrabajados; set => diasTrabajados = value; }
        public decimal SueldoBruto { get => sueldoBruto; set => sueldoBruto = value; }
        public decimal TotalPercepciones { get => totalPercepciones; set => totalPercepciones = value; }
        public decimal TotalDeducciones { get => totalDeducciones; set => totalDeducciones = value; }
        public decimal SueldoNeto { get => sueldoNeto; set => sueldoNeto = value; }
    }
}
