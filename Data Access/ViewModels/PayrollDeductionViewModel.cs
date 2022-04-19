using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class PayrollDeductionViewModel
    {
        private int idDeduccion;
        private string concepto;
        private decimal importe;

        public int IdDeduccion { get => idDeduccion; set => idDeduccion = value; }
        public string Concepto { get => concepto; set => concepto = value; }
        public decimal Importe { get => importe; set => importe = value; }
    }
}
