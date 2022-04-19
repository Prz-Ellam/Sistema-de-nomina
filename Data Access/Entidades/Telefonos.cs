using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Telefonos
    {
        private int idTelefono;
        private string nombre;
        private int idPropietario;

        public int IdTelefono { get => idTelefono; set => idTelefono = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int IdPropietario { get => idPropietario; set => idPropietario = value; }

        public override string ToString()
        {
            return nombre;
        }
    }
}
