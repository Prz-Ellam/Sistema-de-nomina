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
        [RegularExpression(@"^(([A-ZÑ&]{3})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|(([A-ZÑ&]{3})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|(([A-ZÑ&]{3})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|(([A-ZÑ&]{3})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$", ErrorMessage = "RFC no válido")]
        public string Rfc { get => rfc; set => rfc = value; }
        [Required]
        public string RegistroPatronal { get => registroPatronal; set => registroPatronal = value; }
        [Required]
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public bool Activo { get => activo; set => activo = value; }
        public int IdAdministrador { get => idAdministrador; set => idAdministrador = value; }
    }
}
