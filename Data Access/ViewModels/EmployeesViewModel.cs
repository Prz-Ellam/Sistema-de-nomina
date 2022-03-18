using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class EmployeesViewModel
    {
        private int employeeNumber;
        private string name;
        private string fatherLastName;
        private string motherLastName;
        private DateTime dateOfBirth;
        private string curp;
        private string nss;
        private string rfc;
        private string street;
        private string number;
        private string suburb;
        private string city;
        private string state;
        private string postalCode;
        private string bank;
        private int accountNumber;
        private string email;
        //private List<string> phones;
        private string department;
        private string position;
        private DateTime hiringDate;

        public int EmployeeNumber { get => employeeNumber; set => employeeNumber = value; }
        public string Name { get => name; set => name = value; }
        public string FatherLastName { get => fatherLastName; set => fatherLastName = value; }
        public string MotherLastName { get => motherLastName; set => motherLastName = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Curp { get => curp; set => curp = value; }
        public string Nss { get => nss; set => nss = value; }
        public string Rfc { get => rfc; set => rfc = value; }
        public string Street { get => street; set => street = value; }
        public string Number { get => number; set => number = value; }
        public string Suburb { get => suburb; set => suburb = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string PostalCode { get => postalCode; set => postalCode = value; }
        public string Bank { get => bank; set => bank = value; }
        public int AccountNumber { get => accountNumber; set => accountNumber = value; }
        public string Email { get => email; set => email = value; }
        public string Department { get => department; set => department = value; }
        public string Position { get => position; set => position = value; }
        public DateTime HiringDate { get => hiringDate; set => hiringDate = value; }
    }
}
