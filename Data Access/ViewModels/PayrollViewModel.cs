using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

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

        [Name("Numero de empleado")]
        public int EmployeeNumber { get => employeeNumber; set => employeeNumber = value; }

        [Name("Nombre del empleado")]
        public string EmployeeName { get => employeeName; set => employeeName = value; }

        [Name("Fecha")]
        [Format("yyyy-MM")]
        public DateTime Date { get => date; set => date = value; }

        [Name("Cantidad a depositar")]
        [Format("c")]
        public decimal Amount { get => amount; set => amount = value; }

        [Name("Banco")]
        public string Bank { get => bank; set => bank = value; }

        [Name("Número de cuenta")]
        public string AccountNumber { get => accountNumber; set => accountNumber = value; }
    }
}
