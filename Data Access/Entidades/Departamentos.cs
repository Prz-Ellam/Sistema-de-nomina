using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data_Access.Entidades
{
    public class Departamentos
    {
        private int idDepartamento;
        private string nombre;
        private decimal sueldoBase;
        private bool activo;
        private int idEmpresa;

        public int IdDepartamento { get => idDepartamento; set => idDepartamento = value; }
        [Required(ErrorMessage="El nombre del departamento es requerido")]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage="El nombre del departamento solo puede contener letras y espacios")]
        [MaxLength(30, ErrorMessage="El nombre del departamento es muy largo")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public decimal SueldoBase { get => sueldoBase; set => sueldoBase = value; }
        public bool Activo { get => activo; set => activo = value; }
        [Required]
        public int IdEmpresa { get => idEmpresa; set => idEmpresa = value; }
    }
}
