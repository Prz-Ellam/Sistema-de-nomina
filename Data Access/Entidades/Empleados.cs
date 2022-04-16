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
        private int numeroCuenta;
        private string correoElectronico;
        private string contrasena;
        private decimal sueldoDiario;
        private DateTime fechaContratacion;
        private bool activo;
        private int idDepartamento;
        private int idPuesto;

        public int NumeroEmpleado { get => numeroEmpleado; set => numeroEmpleado = value; }
        [Required]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }
        [Required]
        public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }
        [Required]
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        [Required]
        public string Curp { get => curp; set => curp = value; }
        [Required]
        public string Nss { get => nss; set => nss = value; }
        [Required]
        public string Rfc { get => rfc; set => rfc = value; }
        [Required]
        public int Domicilio { get => domicilio; set => domicilio = value; }
        [Required]
        public int Banco { get => banco; set => banco = value; }
        [Required]
        public int NumeroCuenta { get => numeroCuenta; set => numeroCuenta = value; }
        [Required]
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
    }
}
