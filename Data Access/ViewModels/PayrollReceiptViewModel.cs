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
        private DateTime fecha;
        // periodo pago
        private string nombreEmpleado;
        private string nssEmpleado;
        private string rfcEmpleado;
        private string curpEmpleado;
        private int numeroEmpleado;
        private string departamento;
        private string puesto;
        private int diasTrabajados;
        private decimal sueldoDiario;
        private DateTime altaEmpresa;
        private decimal sueldoNeto;
        private List<ConceptViewModel> percepciones;
        private List<ConceptViewModel> deducciones;
    }
}
