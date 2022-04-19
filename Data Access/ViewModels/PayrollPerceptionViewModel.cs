using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class PayrollPerceptionViewModel
    {
        private int idPercepcion;
        private string concepto;
        private decimal importe;

        public int IdPercepcion { get => idPercepcion; set => idPercepcion = value; }
        public string Concepto { get => concepto; set => concepto = value; }
        public decimal Importe { get => importe; set => importe = value; }
    }
}
