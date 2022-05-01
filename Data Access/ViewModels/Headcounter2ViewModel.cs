using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class Headcounter2ViewModel
    {
        private string departamento;
        private uint cantidadEmpleados;

        public string Departamento { get => departamento; set => departamento = value; }
        public uint CantidadEmpleados { get => cantidadEmpleados; set => cantidadEmpleados = value; }
    }
}
