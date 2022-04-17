using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public class DataValidation
    {
        private ValidationContext context;
        private List<System.ComponentModel.DataAnnotations.ValidationResult> results;
        private bool valid;
        private string message;

        public DataValidation(object instance)
        {
            context = new ValidationContext(instance, null, null);
            results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            valid = Validator.TryValidateObject(instance, context, results, true);
            message = "";
        }

        public Tuple<bool, string> Validate()
        {
            if (!valid)
            {
                message += "Revisar los siguientes errores:\n";
                foreach(System.ComponentModel.DataAnnotations.ValidationResult result in results)
                {
                    message += "- " + result.ErrorMessage + "\n";
                }
            }
            return new Tuple<bool, string>(valid, message);
        }

    }
}
