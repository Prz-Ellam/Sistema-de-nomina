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
        private int idEmpresa;
        private string razonSocial;
        private string correoElectronico;
        private string rfc;
        private string registroPatronal;
        private DateTime fechaInicio;
        private bool activo;
        private int idAdministrador;
        private List<string> telefonos = new List<string>();
        // Domicilios
        private string calle;
        private string numero;
        private string colonia;
        private string ciudad;
        private string estado;
        private string codigoPostal;

        public int IdEmpresa { get => idEmpresa; set => idEmpresa = value; }
        [Required(ErrorMessage = "La razón social de la empresa es requerida")]
        public string RazonSocial { get => razonSocial; set => razonSocial = value; }
        [Required(ErrorMessage = "El correo electrónico de la empresa es requerido")]
        [EmailAddress(ErrorMessage = "El correo electrónico que ingresó no es válido")]
        public string CorreoElectronico { get => correoElectronico; set => correoElectronico = value; }
        [Required(ErrorMessage = "El RFC de la empresa es requerido")]
        [RegularExpression(@"^(([A-ZÑ&]{3})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|(([A-ZÑ&]{3})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|(([A-ZÑ&]{3})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|(([A-ZÑ&]{3})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$", ErrorMessage = "RFC no válido")]
        public string Rfc { get => rfc; set => rfc = value; }
        [Required(ErrorMessage = "El registro patronal de la empresa es requerido")]
        public string RegistroPatronal { get => registroPatronal; set => registroPatronal = value; }
        [Required(ErrorMessage = "La fecha de inicio de la empresa es requerida")]
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public bool Activo { get => activo; set => activo = value; }
        [Required]
        public int IdAdministrador { get => idAdministrador; set => idAdministrador = value; }
        [Required(ErrorMessage = "La calle de la empresa es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "La calle solo puede contener letras, números y espacios")]
        public string Calle { get => calle; set => calle = value; }
        [Required(ErrorMessage = "El número de la empresa es requerida")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El número solo puede contener números")]
        public string Numero { get => numero; set => numero = value; }
        [Required(ErrorMessage = "La colonia de la empresa es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "La colonia solo puede contener letras, números y espacios")]
        public string Colonia { get => colonia; set => colonia = value; }
        [Required(ErrorMessage = "La ciudad de la empresa es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "La ciudad solo puede contener letras, números y espacios")]
        public string Ciudad { get => ciudad; set => ciudad = value; }
        [Required(ErrorMessage = "El estado de la empresa es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "El estado solo puede contener letras, números y espacios")]
        public string Estado { get => estado; set => estado = value; }
        [Required(ErrorMessage = "El código postal de la empresa es requerido")]
        [RegularExpression(@"^\d{4,5}$", ErrorMessage = "El codigo postal no es válido")]
        public string CodigoPostal { get => codigoPostal; set => codigoPostal = value; }
        public List<string> Telefonos { get => telefonos; set => telefonos = value; }
    }
}
