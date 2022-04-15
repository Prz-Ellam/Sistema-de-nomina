using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class CompaniesViewModel
    {
        private int idEmpresa;
        private string razonSocial;
        private string calle;
        private string numero;
        private string colonia;
        private string ciudad;
        private string estado;
        private string codigoPostal;
        private string correoElectronico;
        private string registroPatronal;
        private string rfc;
        private DateTime fechaInicio;

        public int IdEmpresa { get => idEmpresa; set => idEmpresa = value; }
        public string RazonSocial { get => razonSocial; set => razonSocial = value; }
        public string Calle { get => calle; set => calle = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Colonia { get => colonia; set => colonia = value; }
        public string Ciudad { get => ciudad; set => ciudad = value; }
        public string Estado { get => estado; set => estado = value; }
        public string CodigoPostal { get => codigoPostal; set => codigoPostal = value; }
        public string CorreoElectronico { get => correoElectronico; set => correoElectronico = value; }
        public string RegistroPatronal { get => registroPatronal; set => registroPatronal = value; }
        public string Rfc { get => rfc; set => rfc = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
    }
}
