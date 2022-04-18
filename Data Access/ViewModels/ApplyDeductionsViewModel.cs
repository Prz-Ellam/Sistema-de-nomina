using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class ApplyDeductionsViewModel
    {
        private bool aplicada;
        private int idDeduccion;
        private string nombre;
        private char tipoMonto;
        private decimal fijo;
        private decimal porcentual;

        public bool Aplicada { get => aplicada; set => aplicada = value; }
        public int IdDeduccion { get => idDeduccion; set => idDeduccion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public char TipoMonto { get => tipoMonto; set => tipoMonto = value; }
        public decimal Fijo { get => fijo; set => fijo = value; }
        public decimal Porcentual { get => porcentual; set => porcentual = value; }
    }
}
