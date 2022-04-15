using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Empresas
    {
        int idEmpresa;
        string razonSocial;
        int domicilio;
        string correoElectronico;
        string rfc;
        string registroPatronal;
        DateTime fechaInicio;
        bool activo;
        int idAdministrador;

        public int IdEmpresa { get => idEmpresa; set => idEmpresa = value; }
        [Required]
        public string RazonSocial { get => razonSocial; set => razonSocial = value; }
        [Required]
        public int Domicilio { get => domicilio; set => domicilio = value; }
        [Required]
        public string CorreoElectronico { get => correoElectronico; set => correoElectronico = value; }
        [Required]
        public string Rfc { get => rfc; set => rfc = value; }
        [Required]
        public string RegistroPatronal { get => registroPatronal; set => registroPatronal = value; }
        [Required]
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public bool Activo { get => activo; set => activo = value; }
        public int IdAdministrador { get => idAdministrador; set => idAdministrador = value; }
    }
}
