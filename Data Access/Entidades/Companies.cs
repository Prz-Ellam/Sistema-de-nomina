using Data_Access.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Companies
    {
        private int companyId;
        private int administratorId;

        private string businessName;
        private string employerRegistration;
        private string rfc;
        private DateTime startDate;
        private string email;
        private List<string> phones = new List<string>();
  
        // Domicilios
        private string street;
        private string number;
        private string suburb;
        private string city;
        private string state;
        private string postalCode;

        public int CompanyId 
        {
            get => companyId;
            set => companyId = value;
        }

        [Required]
        public int AdministratorId
        {
            get => administratorId;
            set => administratorId = value;
        }

        [Required(ErrorMessage = "La razón social de la empresa es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9 ,.&]+$", ErrorMessage = "La razón social que ingresó no es válida")]
        [MaxLength(60, ErrorMessage = "La razón social es demasiado larga")]
        public string BusinessName
        {
            get => businessName;
            set => businessName = value;
        }

        [Required(ErrorMessage = "El registro patronal de la empresa es requerido")]
        [StringLength(11, ErrorMessage = "El registro patronal debe contener 11 caracteres")]
        public string EmployerRegistration
        {
            get => employerRegistration;
            set => employerRegistration = value;
        }

        [Required(ErrorMessage = "El RFC de la empresa es requerido")]
        [RegularExpression(@"^(([A-ZÑ&]{3})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|(([A-ZÑ&]{3})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|(([A-ZÑ&]{3})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|(([A-ZÑ&]{3})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$", ErrorMessage = "El RFC que ingresó no es válido")]
        public string Rfc
        {
            get => rfc;
            set => rfc = value;
        }

        [Required(ErrorMessage = "La fecha de inicio de la empresa es requerida")]
        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
        }

        [Required(ErrorMessage = "El correo electrónico de la empresa es requerido")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "El correo electrónico que ingresó no es válido")]
        [MaxLength(60, ErrorMessage = "El correo electrónico es demasiado largo")]
        public string Email
        {
            get => email;
            set => email = value;
        }

        //[Required]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono no es válido")]
        [PhoneList(ErrorMessage = "Los teléfonos no son válidos")]
        public List<string> Phones { get => phones; set => phones = value; }

        [Required(ErrorMessage = "La calle de la empresa es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "La calle solo puede contener letras, números y espacios")]
        public string Street
        {
            get => street;
            set => street = value;
        }
       
        [Required(ErrorMessage = "El número de la empresa es requerida")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El número solo puede contener números")]
        public string Number
        {
            get => number;
            set => number = value;
        }
        
        [Required(ErrorMessage = "La colonia de la empresa es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "La colonia solo puede contener letras, números y espacios")]
        public string Suburb
        {
            get => suburb;
            set => suburb = value;
        }
        
        [Required(ErrorMessage = "La ciudad de la empresa es requerida")]
        [Blacklist("Seleccionar", ErrorMessage = "La ciudad de la empresa no es válida")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "La ciudad solo puede contener letras, números y espacios")]
        public string City
        {
            get => city;
            set => city = value;
        }
        
        [Required(ErrorMessage = "El estado de la empresa es requerido")]
        [Blacklist("Seleccionar", ErrorMessage = "El estado de la empresa no es válido")]
        [RegularExpression(@"^[a-zA-Z0-9 \u00C0-\u00FF]+$", ErrorMessage = "El estado solo puede contener letras, números y espacios")]
        public string State
        {
            get => state;
            set => state = value;
        }
        
        [Required(ErrorMessage = "El código postal de la empresa es requerido")]
        [RegularExpression(@"^\d{4,5}$", ErrorMessage = "El codigo postal no es válido")]
        public string PostalCode
        {
            get => postalCode;
            set => postalCode = value;
        }
    }
}
