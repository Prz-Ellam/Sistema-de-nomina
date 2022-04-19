using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class PayrollReceiptViewModel
    {
        private string nombreEmpresa;
        private string rfcEmpresa;
        private string registroPatronal;
        private string domicilioFiscalParte1;
        private string domicilioFiscalParte2;
        private int numeroEmpleado;
        private string nombreEmpleado;
        private string nssEmpleado;
        private string curpEmpleado;
        private string rfcEmpleado;
        private DateTime fechaIngreso;
        private string departamento;
        private string puesto;
        //private int diasTrabajados;
        private decimal sueldoDiario;
        private decimal sueldoBruto;
        private decimal sueldoNeto;
        private DateTime periodo;
        private int idNomina;

        public string NombreEmpresa { get => nombreEmpresa; set => nombreEmpresa = value; }
        public string RfcEmpresa { get => rfcEmpresa; set => rfcEmpresa = value; }
        public string RegistroPatronal { get => registroPatronal; set => registroPatronal = value; }
        public string DomicilioFiscalParte1 { get => domicilioFiscalParte1; set => domicilioFiscalParte1 = value; }
        public string DomicilioFiscalParte2 { get => domicilioFiscalParte2; set => domicilioFiscalParte2 = value; }
        public int NumeroEmpleado { get => numeroEmpleado; set => numeroEmpleado = value; }
        public string NombreEmpleado { get => nombreEmpleado; set => nombreEmpleado = value; }
        public string NssEmpleado { get => nssEmpleado; set => nssEmpleado = value; }
        public string CurpEmpleado { get => curpEmpleado; set => curpEmpleado = value; }
        public string RfcEmpleado { get => rfcEmpleado; set => rfcEmpleado = value; }
        public string Departamento { get => departamento; set => departamento = value; }
        public string Puesto { get => puesto; set => puesto = value; }
        public decimal SueldoDiario { get => sueldoDiario; set => sueldoDiario = value; }
        public decimal SueldoBruto { get => sueldoBruto; set => sueldoBruto = value; }
        public decimal SueldoNeto { get => sueldoNeto; set => sueldoNeto = value; }
        public DateTime Periodo { get => periodo; set => periodo = value; }
        public int IdNomina { get => idNomina; set => idNomina = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        //private List<ConceptViewModel> percepciones;
        //private List<ConceptViewModel> deducciones;
    }
}
