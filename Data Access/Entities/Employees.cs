using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class Employees
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
        private int administratorId;
        private DateTime hiringDate;

        public int EmployeeNumber { get => employeeNumber; set => employeeNumber = value; }
        public string Name { get => name; set => name = value; }
        public string FatherLastName { get => fatherLastName; set => fatherLastName = value; }
        public string MotherLastName { get => motherLastName; set => motherLastName = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Curp { get => curp; set => curp = value; }
        public string Nss { get => nss; set => nss = value; }
        public string Rfc { get => rfc; set => rfc = value; }
        public int Address { get => address; set => address = value; }
        public int Bank { get => bank; set => bank = value; }
        public int AccountNumber { get => accountNumber; set => accountNumber = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool Active { get => active; set => active = value; }
        public int DepartmentId { get => departmentId; set => departmentId = value; }
        public int PositionId { get => positionId; set => positionId = value; }
        public int AdministratorId { get => administratorId; set => administratorId = value; }
        public DateTime HiringDate { get => hiringDate; set => hiringDate = value; }
    }
}
