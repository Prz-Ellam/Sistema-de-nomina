using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Positions
    {
        private int positionId;
        private string name;
        private decimal wageLevel;
        private bool active;
        private int companyId;

        public int PositionId { get => positionId; set => positionId = value; }

        [Required(ErrorMessage = "El nombre del puesto es requerido")]
        [RegularExpression(@"^[a-zA-Z \u00C0-\u00FF]+$", ErrorMessage = "El nombre del puesto solo puede contener letras y espacios")]
        [MaxLength(30, ErrorMessage = "El nombre del puesto es muy largo")]
        public string Name { get => name; set => name = value; }

        [Required(ErrorMessage = "El nivel salarial del puesto es requerido")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "El nivel salarial del puesto no puede ser 0")]
        public decimal WageLevel { get => wageLevel; set => wageLevel = value; }

        public bool Active { get => active; set => active = value; }

        [Required]
        public int CompanyId { get => companyId; set => companyId = value; }
    }
}
