using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.ViewModels
{
    public class PayrollViewModel
    {
        private int employeeNumber;
        private string employeeName;
        private DateTime date;
        private decimal amount;
        private string bank;
        private string accountNumber;

        public int EmployeeNumber { get => employeeNumber; set => employeeNumber = value; }
        public string EmployeeName { get => employeeName; set => employeeName = value; }
        public DateTime Date { get => date; set => date = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public string Bank { get => bank; set => bank = value; }
        public string AccountNumber { get => accountNumber; set => accountNumber = value; }
    }
}
