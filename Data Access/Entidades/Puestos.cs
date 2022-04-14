﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Puestos
    {
        private int idPuesto;
        private string nombre;
        private decimal nivelSalarial;
        private bool activo;
        private int idEmpresa;

        public int IdPuesto { get => idPuesto; set => idPuesto = value; }
        [Required(ErrorMessage = "El nombre del departamento es requerido")]
        [RegularExpression(@"[A-Z]", ErrorMessage = "El nombre del departamento solo puede contener letras y espacios")]
        public string Nombre { get => nombre; set => nombre = value; }
        public decimal NivelSalarial { get => nivelSalarial; set => nivelSalarial = value; }
        public bool Activo { get => activo; set => activo = value; }
        public int IdEmpresa { get => idEmpresa; set => idEmpresa = value; }
    }
}
