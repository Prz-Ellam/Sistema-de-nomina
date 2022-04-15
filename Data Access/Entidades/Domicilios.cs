using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Domicilios
    {
        private int idDomicilio;
        private string calle;
        private string numero;
        private string colonia;
        private string ciudad;
        private string estado;
        private string codigoPostal;

        public int IdDomicilio { get => idDomicilio; set => idDomicilio = value; }
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
        [RegularExpression(@"^\d{4,5}$", ErrorMessage ="El codigo postal no es válido")]
        public string CodigoPostal { get => codigoPostal; set => codigoPostal = value; }
    }
}
