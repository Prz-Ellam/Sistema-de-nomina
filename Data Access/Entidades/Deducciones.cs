﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Deducciones
    {
        private int idDeduccion;
        private string nombre;
        private char tipoMonto;
        private decimal fijo;
        private decimal porcentual;
        private char tipoDuracion;
        private bool activo;

        public int IdDeduccion { get => idDeduccion; set => idDeduccion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public char TipoMonto { get => tipoMonto; set => tipoMonto = value; }
        public decimal Fijo { get => fijo; set => fijo = value; }
        public decimal Porcentual { get => porcentual; set => porcentual = value; }
        public char TipoDuracion { get => tipoDuracion; set => tipoDuracion = value; }
        public bool Activo { get => activo; set => activo = value; }
    }
}
