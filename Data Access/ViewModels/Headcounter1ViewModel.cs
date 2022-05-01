using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class Headcounter1ViewModel
    {
        private string departamento;
        private string puesto;
        private uint cantidadEmpleados;

        public string Departamento { get => departamento; set => departamento = value; }
        public string Puesto { get => puesto; set => puesto = value; }
        public uint CantidadEmpleados { get => cantidadEmpleados; set => cantidadEmpleados = value; }
    }
}
