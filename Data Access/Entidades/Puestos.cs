using System;
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
        [Required(ErrorMessage = "El nombre del puesto es requerido")]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage = "El nombre del puesto solo puede contener letras y espacios")]
        [MaxLength(30, ErrorMessage = "El nombre del puesto es muy largo")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public decimal NivelSalarial { get => nivelSalarial; set => nivelSalarial = value; }
        public bool Activo { get => activo; set => activo = value; }
        [Required]
        public int IdEmpresa { get => idEmpresa; set => idEmpresa = value; }
    }
}
