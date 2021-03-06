using Data_Access.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Employees
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
        private List<string> telefonos = new List<string>();

        // Domicilios
        private string calle;
        private string numero;
        private string colonia;
        private string ciudad;
        private string estado;
        private string codigoPostal;

        public int NumeroEmpleado
        { 
            get => numeroEmpleado;
            set => numeroEmpleado = value; 
        }

        [Required(ErrorMessage = "El nombre del empleado es requerido")]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage = "El nombre del empleado solo puede contener letras y espacios")]
        [MaxLength(30, ErrorMessage = "El nombre del empleado no puede tener más de 30 caracteres")]
        public string Nombre 
        { 
            get => nombre;
            set => nombre = value;
        }

        [Required(ErrorMessage = "El apellido paterno del empleado es requerido")]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage = "El apellido paterno del empleado solo puede contener letras y espacios")]
        [MaxLength(30, ErrorMessage = "El nombre del empleado no puede tener más de 30 caracteres")]
        public string ApellidoPaterno 
        {
            get => apellidoPaterno;
            set => apellidoPaterno = value;
        }

        [Required(ErrorMessage = "El apellido materno del empleado es requerido")]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage = "El apellido materno del empleado solo puede contener letras y espacios")]
        [MaxLength(30, ErrorMessage = "El nombre del empleado no puede tener más de 30 caracteres")]
        public string ApellidoMaterno
        {
            get => apellidoMaterno;
            set => apellidoMaterno = value;
        }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        public DateTime FechaNacimiento
        {
            get => fechaNacimiento;
            set => fechaNacimiento = value;
        }

        //[CURP]
        [Required(ErrorMessage = "El CURP del empleado es requerido")]
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage = "El CURP que ingresó no es válido")]
        public string Curp {
            get => curp;
            set => curp = value;
        }

        [Required(ErrorMessage = "El NSS del empleado es requerido")]
        [RegularExpression(@"^(\d{2})(\d{2})(\d{2})\d{5}$", ErrorMessage = "El NSS que ingresó no es válido")]
        public string Nss
        {
            get => nss;
            set => nss = value;
        }

        [Required(ErrorMessage = "El RFC del empleado es requerido")]
        [RegularExpression(@"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$", ErrorMessage = "El RFC que ingresó no válido")]
        public string Rfc
        {
            get => rfc;
            set => rfc = value;
        }

        [Required]
        public int Domicilio { get => domicilio; set => domicilio = value; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El banco del empleado no es válido")]
        public int Banco
        {
            get => banco;
            set => banco = value;
        }

        [Required(ErrorMessage = "El número de cuenta es requerido")]
        [MinLength(16, ErrorMessage = "El número de cuenta debe contener 16 caracteres")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El número de cuenta solo acepta dígitos")]
        public string NumeroCuenta
        {
            get => numeroCuenta;
            set => numeroCuenta = value;
        }

        [Required(ErrorMessage = "El correo electrónico del empleado es requerido")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "El correo electrónico que ingresó no es válido")]
        [MaxLength(60, ErrorMessage = "El correo electrónico es demasiado largo")]
        public string CorreoElectronico
        {
            get => correoElectronico;
            set => correoElectronico = value;
        }

        [Required(ErrorMessage = "La contraseña del empleado es requerida")]
        public string Contrasena
        {
            get => contrasena;
            set => contrasena = value;
        }

        public decimal SueldoDiario { get => sueldoDiario; set => sueldoDiario = value; }
        [Required]
        public DateTime FechaContratacion { get => fechaContratacion; set => fechaContratacion = value; }
        public bool Activo { get => activo; set => activo = value; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El departamento del empleado no es válido")]
        public int IdDepartamento { get => idDepartamento; set => idDepartamento = value; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El puesto del empleado no es válido")]
        public int IdPuesto { get => idPuesto; set => idPuesto = value; }

        [Required(ErrorMessage = "La calle del empleado es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "La calle solo puede contener letras, números y espacios")]
        public string Calle
        {
            get => calle;
            set => calle = value;
        }

        [Required(ErrorMessage = "El número del domicilio del empleado es requerido")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El número solo puede contener números")]
        public string Numero
        {
            get => numero;
            set => numero = value;
        }

        [Required(ErrorMessage = "La colonia del empleado es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "La colonia solo puede contener letras, números y espacios")]
        public string Colonia
        {
            get => colonia;
            set => colonia = value;
        }

        [Required(ErrorMessage = "El municipio del empleado es requerido")]
        [Blacklist("Seleccionar", ErrorMessage = "El municipio del empleado no es válido")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "El municipio solo puede contener letras, números y espacios")]
        public string Ciudad
        {
            get => ciudad;
            set => ciudad = value;
        }

        [Required(ErrorMessage = "El estado del empleado es requerido")]
        [Blacklist("Seleccionar", ErrorMessage = "El estado del empleado no es válido")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "El estado solo puede contener letras, números y espacios")]
        public string Estado { get => estado; set => estado = value; }

        [Required(ErrorMessage = "El código postal del empleado es requerido")]
        [RegularExpression(@"^\d{4,5}$", ErrorMessage = "El codigo postal no es válido")]
        public string CodigoPostal
        {
            get => codigoPostal;
            set => codigoPostal = value;
        }
        //[Required]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono no es válido")]
        [PhoneList(ErrorMessage = "Los teléfonos no son válidos")]
        public List<string> Telefonos { get => telefonos; set => telefonos = value; }
    }
}
