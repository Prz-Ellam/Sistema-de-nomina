using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class PerceptionsAmountType : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Percepciones)validationContext.ObjectInstance;

            if (model.Fijo == 0.0m && model.Porcentual == 0.0m)
            {
                return new ValidationResult("La cantidad de la percepción no puede ser cero");
            }
            else
            {
                if (model.TipoMonto == 'F' && model.Fijo == 0.0m)
                {
                    return new ValidationResult("La cantidad de la percepción no puede ser cero");
                }

                if (model.TipoMonto == 'P' && model.Porcentual == 0.0m)
                {
                    return new ValidationResult("La cantidad de la percepción no puede ser cero");
                }

                return ValidationResult.Success;
            }
        }
    }

    public class Percepciones
    {
        private int idPercepcion;
        private string nombre;
        private char tipoMonto;
        private decimal fijo;
        private decimal porcentual;
        private char tipoDuracion;
        private bool activo;
        private int idEmpresa;

        public int IdPercepcion { get => idPercepcion; set => idPercepcion = value; }
        [Required(ErrorMessage = "El nombre de la percepción es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "La nombre de la percepción solo puede contener letras, números y espacios")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        [RegularExpression("^[FP]$", ErrorMessage = "El tipo de monto es requerido")]
        public char TipoMonto { get => tipoMonto; set => tipoMonto = value; }
        [PerceptionsAmountType]
        public decimal Fijo { get => fijo; set => fijo = value; }
        
        public decimal Porcentual { get => porcentual; set => porcentual = value; }
        public char TipoDuracion { get => tipoDuracion; set => tipoDuracion = value; }
        public bool Activo { get => activo; set => activo = value; }
        public int IdEmpresa { get => idEmpresa; set => idEmpresa = value; }
    }
}
