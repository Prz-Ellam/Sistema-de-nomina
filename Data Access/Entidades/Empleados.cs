using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entidades
{
    public class Empleados
    {
        private int employeeNumber;
        private string name;
        private string fatherLastName;
        private string motherLastName;
        private DateTime dateOfBirth;
        private string curp;
        private string nss;
        private string rfc;
        private int address;
        private int bank;
        private int accountNumber;
        private string email;
        private string password;
        private bool active;
        private int departmentId;
        private int positionId;
        private DateTime hiringDate;

        public int EmployeeNumber { get => employeeNumber; set => employeeNumber = value; }
        [Required(ErrorMessage = "")]
        public string Name { get => name; set => name = value; }
        [Required(ErrorMessage = "")]
        public string FatherLastName { get => fatherLastName; set => fatherLastName = value; }
        [Required(ErrorMessage = "")]
        public string MotherLastName { get => motherLastName; set => motherLastName = value; }
        [Required(ErrorMessage = "")]
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        [Required(ErrorMessage = "")]
        public string Curp { get => curp; set => curp = value; }
        [Required(ErrorMessage = "")]
        public string Nss { get => nss; set => nss = value; }
        [Required(ErrorMessage = "")]
        public string Rfc { get => rfc; set => rfc = value; }
        [Required(ErrorMessage = "")]
        public int Address { get => address; set => address = value; }
        [Required(ErrorMessage = "")]
        public int Bank { get => bank; set => bank = value; }
        [Required(ErrorMessage = "")]
        public int AccountNumber { get => accountNumber; set => accountNumber = value; }
        [Required(ErrorMessage = "")]
        public string Email { get => email; set => email = value; }
        [Required(ErrorMessage = "")]
        public string Password { get => password; set => password = value; }
        public bool Active { get => active; set => active = value; }
        [Required(ErrorMessage = "")]
        public int DepartmentId { get => departmentId; set => departmentId = value; }
        [Required(ErrorMessage = "")]
        public int PositionId { get => positionId; set => positionId = value; }
        [Required(ErrorMessage = "")]
        public DateTime HiringDate { get => hiringDate; set => hiringDate = value; }
    }
}
