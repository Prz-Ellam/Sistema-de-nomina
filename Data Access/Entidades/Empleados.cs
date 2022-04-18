using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Empleados
    {
        private int numeroEmpleado;
        private string nombre;
        private string apellidoPaterno;
        private string apellidoMaterno;
        private DateTime fechaNacimiento;
        private string curp;
        private string nss;
        private string rfc;
        private int domicilio;
        private int banco;
        private string numeroCuenta;
        private string correoElectronico;
        private string contrasena;
        private decimal sueldoDiario;
        private DateTime fechaContratacion;
        private bool activo;
        private int idDepartamento;
        private int idPuesto;

        // Domicilios
        private string calle;
        private string numero;
        private string colonia;
        private string ciudad;
        private string estado;
        private string codigoPostal;

        public int NumeroEmpleado { get => numeroEmpleado; set => numeroEmpleado = value; }
        [Required]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage = "El nombre del puesto solo puede contener letras y espacios")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage = "El nombre del puesto solo puede contener letras y espacios")]
        public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }
        [Required]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage = "El nombre del puesto solo puede contener letras y espacios")]
        public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }
        [Required]
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        [Required]
        public string Curp { get => curp; set => curp = value; }
        [Required]
        public string Nss { get => nss; set => nss = value; }
        [Required]
        //[RegularExpression(@"^(([A-ZÑ&]{4})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|(([A-ZÑ&]{4})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|(([A-ZÑ&]{4})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|(([A-ZÑ&]{4})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$", ErrorMessage = "RFC no válido")]
        public string Rfc { get => rfc; set => rfc = value; }
        [Required]
        public int Domicilio { get => domicilio; set => domicilio = value; }
        [Required]
        public int Banco { get => banco; set => banco = value; }
        [Required]
        public string NumeroCuenta { get => numeroCuenta; set => numeroCuenta = value; }
        [Required]
        [EmailAddress(ErrorMessage = "El correo electrónico que ingresó no es válido")]
        public string CorreoElectronico { get => correoElectronico; set => correoElectronico = value; }
        [Required]
        public string Contrasena { get => contrasena; set => contrasena = value; }
        public decimal SueldoDiario { get => sueldoDiario; set => sueldoDiario = value; }
        [Required]
        public DateTime FechaContratacion { get => fechaContratacion; set => fechaContratacion = value; }
        public bool Activo { get => activo; set => activo = value; }
        [Required]
        public int IdDepartamento { get => idDepartamento; set => idDepartamento = value; }
        [Required]
        public int IdPuesto { get => idPuesto; set => idPuesto = value; }
        [Required]
        public string Calle { get => calle; set => calle = value; }
        [Required]
        public string Numero { get => numero; set => numero = value; }
        [Required]
        public string Colonia { get => colonia; set => colonia = value; }
        [Required]
        public string Ciudad { get => ciudad; set => ciudad = value; }
        [Required]
        public string Estado { get => estado; set => estado = value; }
        [Required]
        [RegularExpression(@"^\d{4,5}$", ErrorMessage = "El codigo postal no es válido")]
        public string CodigoPostal { get => codigoPostal; set => codigoPostal = value; }
    }
}
