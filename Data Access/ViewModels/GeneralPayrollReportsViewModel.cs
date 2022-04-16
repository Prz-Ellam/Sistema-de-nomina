using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class GeneralPayrollReportsViewModel
    {
        private string departamento;
        private string puesto;
        private string nombreEmpleado;
        private DateTime fechaIngreso;
        private uint edad;
        private decimal salarioDiario;

        public string Departamento { get => departamento; set => departamento = value; }
        public string Puesto { get => puesto; set => puesto = value; }
        public string NombreEmpleado { get => nombreEmpleado; set => nombreEmpleado = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public uint Edad { get => edad; set => edad = value; }
        public decimal SalarioDiario { get => salarioDiario; set => salarioDiario = value; }
    }
}
