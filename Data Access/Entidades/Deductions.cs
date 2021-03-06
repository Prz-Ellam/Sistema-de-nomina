using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class DeductionsAmountType : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Deductions)validationContext.ObjectInstance;

            if (model.Fixed == 0.0m && model.Porcentual == 0.0m)
            {
                return new ValidationResult("La cantidad de la deducción no puede ser cero");
            }
            else
            {
                if (model.AmountType == 'F' && model.Fixed == 0.0m)
                {
                    return new ValidationResult("La cantidad de la deducción no puede ser cero");
                }

                if (model.AmountType == 'P' && model.Porcentual == 0.0m)
                {
                    return new ValidationResult("La cantidad de la deducción no puede ser cero");
                }

                return ValidationResult.Success;
            }
        }
    }

    public class Deductions
    {
        private int deductionId;
        private string name;
        private char amountType;
        private decimal _fixed;
        private decimal porcentual;
        private char durationType;
        private bool active;
        private int companyId;

        public int DeductionId { get => deductionId; set => deductionId = value; }

        [Required(ErrorMessage = "El nombre de la deducción es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "La nombre de la deducción solo puede contener letras, números y espacios")]
        [MaxLength(30, ErrorMessage = "El nombre de la deducción es muy largo")]
        public string Name { get => name; set => name = value; }

        [Required]
        [RegularExpression("^[FP]$", ErrorMessage = "El tipo de monto es requerido")]
        public char AmountType { get => amountType; set => amountType = value; }

        [DeductionsAmountType]
        public decimal Fixed { get => _fixed; set => _fixed = value; }
        public decimal Porcentual { get => porcentual; set => porcentual = value; }
        public char DurationType { get => durationType; set => durationType = value; }
        public bool Active { get => active; set => active = value; }

        [Required]
        public int CompanyId { get => companyId; set => companyId = value; }
    }
}
