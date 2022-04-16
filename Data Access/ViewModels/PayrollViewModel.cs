using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class PayrollViewModel
    {
        private int numeroEmpleado;
        private string nombreEmpleado;
        private DateTime fecha;
        private decimal cantidad;
        private string banco;
        private string numeroCuenta;

        public int NumeroEmpleado { get => numeroEmpleado; set => numeroEmpleado = value; }
        public string NombreEmpleado { get => nombreEmpleado; set => nombreEmpleado = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public decimal Cantidad { get => cantidad; set => cantidad = value; }
        public string Banco { get => banco; set => banco = value; }
        public string NumeroCuenta { get => numeroCuenta; set => numeroCuenta = value; }
    }
}
