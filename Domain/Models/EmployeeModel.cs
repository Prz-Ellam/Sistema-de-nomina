using Data_Access.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EmployeeModel
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
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage ="")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage = "")]
        public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }
        [Required]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage = "")]
        public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }
        [Required]
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        [Required]
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage = "")]
        public string Curp { get => curp; set => curp = value; }
        [Required]
        public string Nss { get => nss; set => nss = value; }
        [Required]
        [RegularExpression(@"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$", ErrorMessage = "")]
        public string Rfc { get => rfc; set => rfc = value; }
        [Required]
        public int Domicilio { get => domicilio; set => domicilio = value; }
        [Required]
        public int Banco { get => banco; set => banco = value; }
        [Required]
        public int NumeroCuenta { get => numeroCuenta; set => numeroCuenta = value; }
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage ="")]
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


        private EmployeesRepository employeeRepository = new EmployeesRepository();
        private RepositorioDomicilios addressesRepository = new RepositorioDomicilios();


        public EmployeeModel()
        {
            employeeRepository = new EmployeesRepository();
            addressesRepository = new RepositorioDomicilios();
        }


        public string SaveChanges()
        {
            return null;
        }








    }
}
