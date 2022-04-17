using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public class ValidationResult
    {
        private string message;
        private ValidationState state;

        public string Message { get => message; set => message = value; }
        public ValidationState State { get => state; set => state = value; }
    }
}
