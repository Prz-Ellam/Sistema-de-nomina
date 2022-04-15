﻿using System;
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
        public string Nombre { get => nombre; set => nombre = value; }
        public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }
        public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public string Curp { get => curp; set => curp = value; }
        public string Nss { get => nss; set => nss = value; }
        public string Rfc { get => rfc; set => rfc = value; }
        public int Domicilio { get => domicilio; set => domicilio = value; }
        public int Banco { get => banco; set => banco = value; }
        public int NumeroCuenta { get => numeroCuenta; set => numeroCuenta = value; }
        public string CorreoElectronico { get => correoElectronico; set => correoElectronico = value; }
        public string Contrasena { get => contrasena; set => contrasena = value; }
        public decimal SueldoDiario { get => sueldoDiario; set => sueldoDiario = value; }
        public DateTime FechaContratacion { get => fechaContratacion; set => fechaContratacion = value; }
        public bool Activo { get => activo; set => activo = value; }
        public int IdDepartamento { get => idDepartamento; set => idDepartamento = value; }
        public int IdPuesto { get => idPuesto; set => idPuesto = value; }
    }
}
