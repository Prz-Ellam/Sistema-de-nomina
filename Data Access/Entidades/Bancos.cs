using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Bancos
    {
        private int idBanco;
        private string nombre;

        public int IdBanco { get => idBanco; set => idBanco = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
